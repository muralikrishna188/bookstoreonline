using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using cart.Data;
using cart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace cart.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookContext _context;
        private readonly IConfiguration _config;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config , BookContext context)
        {
            _logger = logger;
            _context=context;   
            _config=config;        
        }

        public void OnGet()
        {

            List<Book> books=new List<Book>();

            // UNCOMMENT AFTER WE HAVE A DATABASE  
            //books=GetBooksInShoppingCart();

            ViewData["books"]=books;
        }

        public IActionResult OnPostRemoveFromShoppingCart()  {
            var bookId=int.Parse(Request.Form["bookId"]);
            var book=_context.Books.Find(bookId);
            book.InStock++;
            _context.SaveChanges();
            
            var client=GetRedisClient();
            client.RemoveItemFromList("cart", bookId.ToString());            

            return RedirectToPage();
        }  

        public void OnPostPlaceOrder()  {
            
            var booksInCart=GetBooksInShoppingCart();
            var order=new Order();
            double total=0;
            order.orderDate=DateTime.Now;
            order.priority=1;
            order.items=new List<OrderItem>();

            foreach (var book in booksInCart)  {
                var item=new OrderItem();
                item.name=book.Name;
                item.id=book.ID;
                item.price=book.Price;
                total+=item.price;
                order.items.Add(item);
            }

            order.total=total;

            var json=JsonConvert.SerializeObject(order);

            Console.WriteLine(json);

            try  {
            var client=new HttpClient();
            var result=client.PostAsync(_config.GetValue<String>("OrderFunctionUrl"),new StringContent(json)).Result;
                     
            Console.WriteLine("Order sent, status:" + result);

            if (result.StatusCode==System.Net.HttpStatusCode.NoContent)  {
                ClearCart();
            }

            ViewData["books"]=new List<Book>();
            ViewData["OrderStatus"]="sent";
            }
            catch (Exception ex)  {
                ViewData["OrderStatus"]="Error sending order: " + ex.Message;
            }
        }         

        private List<Book> GetBooksInShoppingCart()  {

            var client=GetRedisClient();
            var bookIDs=client.GetAllItemsFromList("cart");
                 
            return _context.Books.Where(b=>bookIDs.Contains(b.ID.ToString())).ToList();
        }

        private void ClearCart()  {

            var client=GetRedisClient();
            client.RemoveAllFromList("cart");
        }

        private IRedisClient GetRedisClient()
        {
            var conString = _config.GetValue<String>("Redis:ConnectionString");
            var manager = new RedisManagerPool(conString);
            return manager.GetClient();
        }
    }

    class Order  {
        public int orderId  {get; set;}
        public DateTime orderDate  {get; set;}
        public double total { get; set; }
        public int priority  {get; set;}
        public List<OrderItem> items { get; set; }

        public Order()
        {
            orderId=new Random().Next(1000);
        }
    }

    class OrderItem  {
        public String name { get; set; }
        public int id { get; set; }
        public double price { get; set; }
    }
}

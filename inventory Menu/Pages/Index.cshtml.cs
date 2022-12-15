using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory.Data;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace inventory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookContext _context;

        public IndexModel(ILogger<IndexModel> logger, BookContext context)
        {
            _logger = logger;
            _context=context;
        }

        public void OnGet()
        {
            List<Book> books=null;
            
            // UNCOMMENT AFTER SETTING THE CONNECTION STRING
            // try  {
            //     books=_context.Books.ToList();
            // }
            // catch (Exception ex)  {
            //     ViewData["Error"]=ex.Message;
            // }
            ViewData["books"]=books;

        }

        public IActionResult OnPostSaveInventory()  {
            var bookId=int.Parse(Request.Form["bookId"]);
            var quantity=int.Parse(Request.Form["book.InStock"]);
            var book=_context.Books.Find(bookId);
            book.InStock=quantity;
            _context.SaveChanges();           

            return RedirectToPage();
        }      
    }
}

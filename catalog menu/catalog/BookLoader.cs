using System.Collections.Generic;
using catalog.Data;
using catalog.Models;
using System.Linq;

namespace catalog
{
    public class BookLoader
    {
        public static void LoadBooks(BookContext ctx)
        {
            // First - remove all books from the DB
            ctx.Books.RemoveRange(ctx.Books);

            // Now - create list of books
            var books = new List<Book>
            {
                new Book{Name="Rama II", Author="Arthur C. Clark", Pages=281,ImageUrl="rama.jpg", InStock=54, Price=44.23 },
                new Book{Name="Exhalation", Author="Ted Chiang", Pages=556,ImageUrl="exhalation.jpg", InStock=13, Price=50.99 },
                new Book{Name="Traffic Secrets", Author="Russell Brunson", Pages=306,ImageUrl="traffic.jpg", InStock=65, Price=18.97 },
                new Book{Name="Clean Code", Author="Robert Martin", Pages=464,ImageUrl="clean.jpg", InStock=15, Price=87.00 }
            };

            // Add the book to the context
            ctx.Books.AddRange(books);

            // Save changes to the DB
            ctx.SaveChanges();
            System.Console.WriteLine($"Loaded {ctx.Books.Count()} books");
        }
    }
}
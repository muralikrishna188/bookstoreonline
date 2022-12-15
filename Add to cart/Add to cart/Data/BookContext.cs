using cart.Models;
using Microsoft.EntityFrameworkCore;

namespace cart.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Book> Books  {get; set;}  
        
    }    
}
    
        
using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions options):base(options) { 
        
        }

        public DbSet<Shops> Shops {  get; set; } 
    }
}

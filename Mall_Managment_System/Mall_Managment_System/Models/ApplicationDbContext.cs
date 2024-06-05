using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

            
            
        }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Foodcourt> FoodCourt { get; set; }
        public DbSet<Gallary> Gallary { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<FoodItems> FoodItems { get; set; }
    }
}

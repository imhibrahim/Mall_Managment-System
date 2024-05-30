using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {

        }

        public DbSet<Items> Items { get; set; }
    }
}

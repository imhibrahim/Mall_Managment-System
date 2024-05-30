using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class FoodCourtDbContext : DbContext
    {
        public FoodCourtDbContext(DbContextOptions<FoodCourtDbContext> options) : base(options)
        {

        }

        public DbSet<Foodcourt> FoodCourt { get; set; }
    }
}

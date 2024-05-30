using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

        public DbSet<Movies> Movies { get; set; }
    }
}

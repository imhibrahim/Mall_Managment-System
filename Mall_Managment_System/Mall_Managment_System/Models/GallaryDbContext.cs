using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Models
{
    public class GallaryDbContext : DbContext
    {
    
    public GallaryDbContext(DbContextOptions<GallaryDbContext> options) : base(options)
    {

    }


    public DbSet<Gallary> Gallary { get; set; }
}
}

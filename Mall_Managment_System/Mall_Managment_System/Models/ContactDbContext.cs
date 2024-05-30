using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Mall_Managment_System.Models
{
    public class ContactDbContext :DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> contact { get; set; }
    }
}

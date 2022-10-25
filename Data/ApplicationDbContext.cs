using DemoMVC2.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC2.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students {get; set;}
        public DbSet<DemoMVC2.Models.Person>? Person { get; set; }
        public DbSet<DemoMVC2.Models.Employee>? Employee { get; set; }
        public DbSet<DemoMVC2.Models.Customer>? Customer { get; set; }
    }
}
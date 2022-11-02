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
        public DbSet<Employee> Employees {get; set;}
       
    }
}
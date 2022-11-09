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
        public DbSet<Person> Persons {get; set;}
        public DbSet<Customer> Customers {get; set;}
        public DbSet<Khachhang> Khachhangs {get; set;}
        public DbSet<DemoMVC2.Models.Sinhvien>? Sinhvien { get; set; }
       
    }
}
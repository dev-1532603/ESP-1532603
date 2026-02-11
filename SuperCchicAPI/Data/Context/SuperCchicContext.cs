using Microsoft.EntityFrameworkCore;
using SuperCchicLibrary;

namespace SuperCchicAPI.Data.Context
{
    public class SuperCchicContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Details> Order_Details { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public SuperCchicContext(DbContextOptions<SuperCchicContext> options): base(options)
        {
        }  
    }
}

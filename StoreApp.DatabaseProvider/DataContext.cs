using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider.Models;

namespace StoreApp.DatabaseProvider
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}

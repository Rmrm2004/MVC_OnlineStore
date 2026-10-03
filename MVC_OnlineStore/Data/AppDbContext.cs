using Microsoft.EntityFrameworkCore;
using MVC_OnlineStore.Models;

namespace MVC_OnlineStore.Data
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=OnlineStoreDB;Trusted_Connection=True;TrustServerCertificate=True;encrypt=false");

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().
                 Property(p => p.Price).
                 HasPrecision(18,2);
            modelBuilder.Entity<Product>().
                HasOne(p => p.Category).
                WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Order>().
             Property(p => p.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().
                Property(p => p.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().
              HasOne(p => p.Order).WithMany(p => p.OrderItems);
            




        }  
    }


}

using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 1, Name = "Shirt", Price = 20, Quantity = 20 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 2, Name = "Pants", Price = 30, Quantity = 50 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 3, Name = "Shoes", Price = 50, Quantity = 10 });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}


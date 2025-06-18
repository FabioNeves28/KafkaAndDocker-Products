using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Ecommerce.ProductService.Services.Product;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Tests.Product
{
    public class ProductControllerTests
    {
        private ProductDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseSqlServer("Data Source=localhost;Initial Catalog=EcommerceProduct;Integrated Security=True;TrustServerCertificate=True")
                .Options;

            return new ProductDbContext(options);
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var dbContext = GetDbContext();
            dbContext.Products.AddRange(new List<ProductModel>
                {
                    new ProductModel { Name = "Produto 1", Price = 10, Quantity = 5 },
                    new ProductModel { Name = "Produto 2", Price = 20, Quantity = 3 }
                });
            dbContext.SaveChanges();

            var service = new ProductsService(dbContext);

            // Act
            var result = await service.GetProductsAsync();

            // Assert
            Assert.Contains(result, p => p.Name == "Produto 1");
            Assert.Contains(result, p => p.Name == "Produto 2");
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenExists()
        {
            // Arrange
            var dbContext = GetDbContext();
            var product = new ProductModel { Name = "Produto Teste", Price = 15, Quantity = 2 };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = new ProductsService(dbContext);

            // Act
            var result = await service.GetProductAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Produto Teste", result.Name);
        }

        [Fact]
        public async Task GetProduct_ReturnsNull_WhenNotExists()
        {
            // Arrange
            var dbContext = GetDbContext();
            var service = new ProductsService(dbContext);

            // Act
            var result = await service.GetProductAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}

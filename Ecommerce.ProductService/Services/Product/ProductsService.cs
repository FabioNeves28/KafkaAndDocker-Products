using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Services.Product
{
    public class ProductsService
    {
        private readonly ProductDbContext _dbContext;
        public ProductsService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductModel>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<ProductModel> GetProductAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

    }
}

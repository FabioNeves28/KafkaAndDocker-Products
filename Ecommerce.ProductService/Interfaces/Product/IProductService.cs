using Ecommerce.Model;

namespace Ecommerce.ProductService.Interfaces.Product
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<ProductModel> GetProductAsync(int id);
        Task<ProductModel> CreateProductAsync(ProductModel product);
    }
}

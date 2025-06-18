using Ecommerce.Model;
using Ecommerce.ProductService.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productSerivce) : ControllerBase
    {
        [HttpGet]
        public async Task<List<ProductModel>> GetProduct() => await productSerivce.GetProductsAsync();

        [HttpPost]
        public async Task<ProductModel> GetProduct(int productId) => await productSerivce.GetProductAsync(productId);
    }
}

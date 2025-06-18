using Ecommerce.Model;
using Ecommerce.OrderService.Interfaces.Order;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<List<OrderModel>> GetOrders() => await orderService.GetOrdersAsync();

        [HttpPost]
        public async Task<OrderModel> CreateOrder(OrderModel order) => await orderService.CreateOrderAsync(order);
    }
}

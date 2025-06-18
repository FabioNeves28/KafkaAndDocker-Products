using Ecommerce.Model;

namespace Ecommerce.OrderService.Interfaces.Order
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetOrdersAsync();
        Task<OrderModel> CreateOrderAsync(OrderModel order);
    }
}

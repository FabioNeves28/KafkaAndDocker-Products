using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Interfaces.Order;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        public OrderService(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<OrderModel>> GetOrdersAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }
        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}

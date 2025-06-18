using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Interfaces.Kafka;
using Ecommerce.OrderService.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text.Json;

public class OrderControllerTests
{
    private OrderDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseSqlServer("Data Source=localhost;Initial Catalog=EcommerceOrder;Integrated Security=True;TrustServerCertificate=True")
            .Options;

        return new OrderDbContext(options);
    }

    [Fact]
    public async Task GetOrders_ReturnsAllOrders()
    {
        // Arrange
        var dbContext = GetDbContext();
        dbContext.Orders.Add(new OrderModel { CustomerName = "Cliente 1", ProductId = 1, Quantity = 2, OrderDate = DateTime.Now });
        dbContext.Orders.Add(new OrderModel { CustomerName = "Cliente 2", ProductId = 2, Quantity = 1, OrderDate = DateTime.Now });
        dbContext.SaveChanges();

        var service = new OrderService(dbContext);

        // Act
        var result = await service.GetOrdersAsync();

        // Assert
        Assert.Contains(result, o => o.CustomerName == "Cliente 1");
        Assert.Contains(result, o => o.CustomerName == "Cliente 2");
    }

    [Fact]
    public async Task CreateOrder_AddsOrderAndProducesKafkaMessage()
    {
        // Arrange
        var dbContext = GetDbContext();
        var producerMock = new Mock<IKafkaProducer>();
        producerMock
            .Setup(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<string, string>>()))
            .Returns(Task.CompletedTask);

        var service = new OrderService(dbContext);
        Random rnd = new Random();
        var order = new OrderModel
        {
            CustomerName = "Novo Cliente",
            ProductId = rnd.Next(99),
            Quantity = 5
        };

        // Act
        var result = await service.CreateOrderAsync(order);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Novo Cliente", result.CustomerName);

        var dbOrder = dbContext.Orders.FirstOrDefault(o => o.Id == result.Id);
        Assert.NotNull(dbOrder);

        producerMock.Verify(p => p.ProduceAsync(
           "order-topic",
           It.Is<Message<string, string>>(m => m.Key == result.Id.ToString() && m.Value == JsonSerializer.Serialize(result, new JsonSerializerOptions()))
        ), Times.Once);
    }
}

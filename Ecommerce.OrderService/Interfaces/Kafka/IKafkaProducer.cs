using Confluent.Kafka;

namespace Ecommerce.OrderService.Interfaces.Kafka
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}

﻿using Confluent.Kafka;
using Ecommerce.OrderService.Interfaces.Kafka;

namespace Ecommerce.OrderService.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        public KafkaProducer()
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }
        public Task ProduceAsync(string topic, Message<string, string> message)
        {
             return _producer.ProduceAsync(topic, message);
        }
    }
}

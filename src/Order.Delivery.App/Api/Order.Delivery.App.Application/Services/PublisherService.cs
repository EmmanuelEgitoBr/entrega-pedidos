using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Order.Delivery.App.Application.Services.Interfaces;
using System.Text.Json;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Services;

public class PublisherService : IPublisherService
{
    private readonly IConfiguration _configuration;
    private readonly string _orderTopicName;
    private readonly string _kafkaBootstrapServers;

    public PublisherService(IConfiguration configuration)
    {
        _configuration = configuration;
        _orderTopicName = _configuration["KafkaSettings:OrderTopicName"]!;
        _kafkaBootstrapServers = _configuration["KafkaSettings:KafkaBootstrapServer"]!;
    }

    public async Task PublishMessageToTopicAsync(Entity.Order order)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _kafkaBootstrapServers,
        };
        string orderConvertedToJson = JsonSerializer.Serialize(order);

        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var deliveryReport = await producer.ProduceAsync(_orderTopicName, new Message<Null, string> { Value = orderConvertedToJson });
    }
}

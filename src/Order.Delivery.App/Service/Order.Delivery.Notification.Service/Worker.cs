using Confluent.Kafka;
using Order.Delivery.Notification.Service.Services;
using Order.Delivery.Notification.Service.Services.Interfaces;
using System.Text.Json;
using Entity = Order.Delivery.Notification.Service.Models;

namespace Order.Delivery.Notification.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;
        private readonly string _notifierConsumeGroupName;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _orderTopicName = _configuration["KafkaSettings:OrderTopicName"]!;
            _kafkaBootstrapServers = _configuration["KafkaSettings:KafkaBootstrapServer"]!;
            _notifierConsumeGroupName = _configuration["KafkaSettings:NotifierConsumeGroupName"]!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            INotifierService notifierService = new NotifierService();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Order notifier running at: {time}", DateTimeOffset.Now);

                    var config = new ConsumerConfig
                    {
                        BootstrapServers = _kafkaBootstrapServers,
                        GroupId = _notifierConsumeGroupName,
                        AutoOffsetReset = AutoOffsetReset.Earliest
                    };

                    using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                    consumer.Subscribe(_orderTopicName);
                    CancellationTokenSource cts = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = true;
                        cts.Cancel();
                    };

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                var consumeResult = consumer.Consume(cts.Token);
                                Entity.Order order = JsonSerializer.Deserialize<Entity.Order>(consumeResult!.Message!.Value)!;
                                notifierService.Notify(order);

                                _logger.LogInformation($"Mensagem recebida: {consumeResult.Message.Value}", DateTimeOffset.Now);
                            }
                            catch (ConsumeException ex) 
                            {
                                _logger.LogError($"Erro ao consumir mensagem: {ex.Error.Reason}");
                            }
                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                        _logger.LogError($"Processo encerrado com a falha: {ex.Message}");
                        consumer.Close();
                    }
                }
            }
        }
    }
}

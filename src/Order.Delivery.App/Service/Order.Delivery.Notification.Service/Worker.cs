using Confluent.Kafka;
using Order.Delivery.Notification.Service.Models;
using Order.Delivery.Notification.Service.Services;
using Order.Delivery.Notification.Service.Services.Interfaces;
using System.Text.Json;

namespace Order.Delivery.Notification.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;
        private readonly string _notifierConsumeGroupName;

        public Worker(ILogger<Worker> logger, 
            IEmailService emailService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
            _orderTopicName = _configuration["KafkaSettings:OrderTopicName"]!;
            _kafkaBootstrapServers = _configuration["KafkaSettings:KafkaBootstrapServer"]!;
            _notifierConsumeGroupName = _configuration["KafkaSettings:NotifierConsumeGroupName"]!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //INotifierService notifierService = new NotifierService();

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
                                OrderMessage order = JsonSerializer.Deserialize<OrderMessage>(consumeResult!.Message!.Value)!;

                                if (order != null)
                                {
                                    //notifierService.Notify(order);

                                    var response = await _emailService.SendEmailAsync(order);

                                    if (response.IsSuccessStatusCode && response.Content!.Success)
                                    {
                                        _logger.LogInformation("Email enviado com sucesso para {Email}", order.Email);
                                    }
                                    else
                                    {
                                        _logger.LogError("Falha ao enviar email: {StatusCode} - {Error}",
                                            response.StatusCode, response.Content?.Error);
                                    }

                                    _logger.LogInformation($"Ação a realizada: {order.Action}", DateTimeOffset.Now);
                                }
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

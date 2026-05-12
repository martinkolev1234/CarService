using CarService.DL.Kafka;
using CarService.Models.Responses;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CarService.DL.Infrastructure.HostedServices
{
    internal class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;
        private readonly KafkaSettings _kafkaSettings;

        public BackgroundWorker(ILogger<BackgroundWorker> logger, KafkaSettings kafkaSettings)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                var consumer = new GenericKafkaConsumer<string, SellCarResult>(_kafkaSettings, (key, value) =>
                {
                    _logger.LogInformation($"[KAFKA] Car sold! Car: {value.Car.Model}, Customer: {value.Customer.Name}, Price: {value.Price}");
                });

                consumer.StartConsuming(stoppingToken);

            }, stoppingToken);
        }
    }
}
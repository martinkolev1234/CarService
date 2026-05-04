using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CarService.Models.Responses; 
using CarService.DL.Kafka;         

namespace CarService.DL.Infrastructure.HostedServices
{
    public class KafkaConsumerWorker : BackgroundService
    {
        private readonly ILogger<KafkaConsumerWorker> _logger;

        public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Kafka Consumer Worker is starting in the background.");

            return Task.Run(() => RunConsumerLoop(stoppingToken), stoppingToken);
        }

        private void RunConsumerLoop(CancellationToken stoppingToken)
        {
            var settings = new KafkaSettings
            {
                BootstrapServers = "kafka-210718-0.cloudclusters.net:10020",
                SaslUsername = "puchat",
                SaslPassword = "1234567q",
                Topic = "pu-chat",
                GroupId = "CarServiceGroup"
            };

            var consumer = new GenericKafkaConsumer<string, SellCarResult>(
                settings,
                onMessageReceived: (key, receivedModel) =>
                {

                    string carInfo = receivedModel.Car != null
                        ? $"{receivedModel.Car.Year} {receivedModel.Car.Model}"
                        : "Unknown Car";

                    string customerName = receivedModel.Customer != null
                        ? receivedModel.Customer.Name
                        : "Unknown Customer";

                    _logger.LogInformation(
                        "\n====== NEW CAR SALE RECEIVED FROM KAFKA ======\n" +
                        "Kafka Key: {Key}\n" +
                        "Customer:  {CustomerName}\n" +
                        "Car:       {CarInfo}\n" +
                        "Sale Price:${Price}\n" +
                        "==============================================",
                        key, customerName, carInfo, receivedModel.Price);
                }
            );

            consumer.StartConsuming(stoppingToken);

            _logger.LogInformation("Kafka Consumer Worker has cleanly stopped.");
        }
    }
}
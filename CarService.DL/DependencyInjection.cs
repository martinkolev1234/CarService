using CarService.DL.Infrastructure.HostedServices;
using CarService.DL.Interfaces;
using CarService.DL.Kafka;
using CarService.DL.Repositories;
using CarService.Models.Configurations;
using CarService.Models.Responses;
using CarService3.DL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configs)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            services
                .AddHostedService<BackgroundWorker>()
                .AddHostedService<HostedWorker>()
                .AddConfigurations(configs)
                .AddSingleton<ICarRepository, CarLocalRepository>()
                .AddSingleton<ICarRepository, CarMongoRepository>()
                .AddSingleton<ICustomerRepository, CustomerMongoRepository>();

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<MongoDbConfiguration>(configs.GetSection(nameof(MongoDbConfiguration)));

            // REGISTER KafkaSettings
            var kafkaSettings = configs.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();
            services.AddSingleton(kafkaSettings);

            // REGISTER Kafka Producer with SellCarResult
            services.AddSingleton(sp => new GenericKafkaProducer<string, SellCarResult>(kafkaSettings));

            services.AddHostedService<KafkaConsumerWorker>();

            return services;
        }
    }
}
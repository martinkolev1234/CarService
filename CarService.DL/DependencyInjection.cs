using CarService.DL.Infrastructure.HostedServices;
using CarService.DL.Interfaces;
using CarService.DL.Repositories;
using CarService.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
    AddDataLayer(this IServiceCollection services, IConfiguration configs)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            // Register data layer services here
            services
                .AddHostedService<BackgroundWorker>()
                .AddHostedService<HostedWorker>()
                .AddConfigurations(configs)
                .AddSingleton<ICarRepository, CarLocalRepository>()
                .AddSingleton<ICarRepository, CarMongoRepository>()
                .AddSingleton<ICustomerRepository, CustomerMongoRepository>();
                
            return services;
        }
        private static IServiceCollection
            AddConfigurations(this IServiceCollection services, IConfiguration configs) 
        {
            // Register data layer services here
            services.Configure<MongoDbConfiguration>(configs.GetSection(nameof(MongoDbConfiguration)));

            return services;
        }

    }
}

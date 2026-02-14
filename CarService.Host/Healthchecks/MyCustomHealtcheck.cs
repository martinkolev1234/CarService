using CarService.Models.Configurations;
using CarService.Models.Dto;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.Host.Healthchecks
{
    public class MyCustomHealtcheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;

        public MyCustomHealtcheck(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var isHealthy = false;

            try
            {
                var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

                var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

                //var carsCollection = database.GetCollection<Car>($"{nameof(Car)}s");

                database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait(cancellationToken);

                isHealthy = true;
            }
            catch (Exception)
            {
                //log...
                isHealthy = false;
            }


            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("MongoDb is healthy."));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "MongoDb is unhealthy."));
        }
    }
}

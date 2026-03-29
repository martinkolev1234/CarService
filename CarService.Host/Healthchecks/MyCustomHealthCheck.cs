using CarService.Models.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.Host.Healthchecks
{
    public class MyCustomHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MongoDbConfiguration>? _mongoDbConfiguration;

        public MyCustomHealthCheck(IOptionsMonitor<MongoDbConfiguration>? mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var isHealthy = false;

            try
            {
                var client = new MongoClient(_mongoDbConfiguration?.CurrentValue.ConnectionString);
                var database = client.GetDatabase(_mongoDbConfiguration?.CurrentValue.DatabaseName);

                await database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}", cancellationToken: cancellationToken);
                isHealthy = true;
            }
            catch (Exception)
            {
                isHealthy = false;
            }

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("MongoDB is healthy.");
            }

            return new HealthCheckResult(
                context.Registration.FailureStatus, "MongoDB is unhealthy.");
        }
    }
}
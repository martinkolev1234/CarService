using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Infrastructure.HostedServices
{
    internal class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;

        public BackgroundWorker(ILogger<BackgroundWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Test BackgroundWorker: {DateTime.UtcNow}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
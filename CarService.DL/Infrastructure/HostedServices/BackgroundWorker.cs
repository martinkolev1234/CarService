using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarService.DL.Infrastructure.HostedServices
{
    internal class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;

        public BackgroundWorker(ILogger<BackgroundWorker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Test BackgroundWorker: {DateTime.UtcNow}");
                    await Task.Delay(1000, stoppingToken);
                }
            }, stoppingToken);
        }
    }
}

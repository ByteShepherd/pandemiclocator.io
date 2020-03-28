using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using events.pandemiclocator.io;
using infra.api.pandemiclocator.io.Implementations;
using infra.api.pandemiclocator.io.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.pandemiclocator.io.Infra.Services
{
    //Fonte: https://www.c-sharpcorner.com/article/consuming-rabbitmq-messages-in-asp-net-core/
    public class HealthReportConsumerService : BackgroundService
    {
        private readonly IHealthReportFactoryProvider _healthReportFactoryProvider;
        private readonly ILogger _logger;

        public HealthReportConsumerService(ILoggerFactory loggerFactory, IHealthReportFactoryProvider healthReportFactoryProvider)
        {
            _healthReportFactoryProvider = healthReportFactoryProvider;
            _logger = loggerFactory.CreateLogger<HealthReportConsumerService>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new NewHealthReportConsumer(_logger, _healthReportFactoryProvider.Channel);
            _healthReportFactoryProvider.Channel.BasicConsume(ChannelExtensions.HealthReportQueueName, false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _healthReportFactoryProvider?.Dispose();
            base.Dispose();
        }
    }
}

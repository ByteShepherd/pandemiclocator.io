using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using events.pandemiclocator.io;
using infra.api.pandemiclocator.io.Implementations;
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
            this._logger = loggerFactory.CreateLogger<HealthReportConsumerService>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new NewHealthReportConsumer(_healthReportFactoryProvider.Channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);

                // handle the received message  
                HandleMessage(content);
                _healthReportFactoryProvider.Channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _healthReportFactoryProvider.Channel.BasicConsume("demo.queue.log", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _healthReportFactoryProvider?.Dispose();
            base.Dispose();
        }
    }
}

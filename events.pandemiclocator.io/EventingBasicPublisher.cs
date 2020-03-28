using System;
using System.Text;
using System.Text.Json;
using infra.api.pandemiclocator.io.Interfaces;
using infra.api.pandemiclocator.io.Queue;
using Microsoft.Extensions.Logging;

namespace events.pandemiclocator.io
{
    public class EventingBasicPublisher<T> : IEventingBasicPublisher<T> where T : class
    {
        private readonly ILogger _logger;
        protected IHealthReportFactoryProvider HealthReportFactoryProvider { get; }

        public EventingBasicPublisher(ILogger logger, IHealthReportFactoryProvider healthReportFactoryProvider)
        {
            _logger = logger;
            HealthReportFactoryProvider = healthReportFactoryProvider;
        }

        protected virtual string PublisherName => this.GetType().Name;

        public virtual QueuePublishResult Publish(T message)
        {
            try
            {
                _logger.LogInformation($"Publisher {nameof(PublisherName)} will publish {message?.GetType().Name}");
                var channel = HealthReportFactoryProvider.Channel;
                var properties = channel.CreateBasicProperties();
                var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                channel.BasicPublish(
                    QueueHealthReportChannelExtensions.HealthReportExchangeName,
                    QueueHealthReportChannelExtensions.HealthReportRouteKey,
                    true,
                    properties,
                    messageBytes);

                return new QueuePublishResult(true);
            }
            catch(Exception err)
            {
                _logger.LogInformation($"Publisher {PublisherName} publish error {err}");
                return new QueuePublishResult(false, err);
            }
        }
    }
}
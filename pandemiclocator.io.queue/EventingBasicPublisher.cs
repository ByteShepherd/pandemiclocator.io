using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.queue.abstractions;

namespace pandemiclocator.io.queue
{
    //Source: https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html / https://github.com/rabbitmq/rabbitmq-tutorials/blob/master/dotnet/PublisherConfirms/PublisherConfirms.cs
    public abstract class EventingBasicPublisher<T> : IEventingBasicPublisher<T> where T : class
    {
        private readonly ILogger _logger;
        protected ILoggerFactory LoggerFactory { get; }
        protected IHealthReportFactoryProvider HealthReportFactoryProvider { get; }

        protected EventingBasicPublisher(ILoggerFactory loggerFactory, IHealthReportFactoryProvider healthReportFactoryProvider)
        {
            LoggerFactory = loggerFactory;
            _logger = InitializeLogger();
            HealthReportFactoryProvider = healthReportFactoryProvider;
        }

        protected abstract ILogger InitializeLogger();
        protected abstract string PublisherName { get; }
    
        public virtual Task<QueuePublishResult> PublishAsync(T message, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _logger.LogInformation($"Publisher {nameof(PublisherName)} will publish {message?.GetType().Name}");
                    var channel = HealthReportFactoryProvider.Channel;

                    var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                    channel.BasicPublish(
                        QueueHealthReportChannelExtensions.HealthReportExchangeName,
                        QueueHealthReportChannelExtensions.HealthReportRouteKey,
                        true,
                        HealthReportFactoryProvider.ChannelBasicProperties,
                        messageBytes);

                    try
                    {
                        //SinceWe are using channel.ConfirmSelect() for Publisher confirms in QueueHealthReportChannelExtensions / InitializeChannelForHealthReport
                        //publish a message as usual and wait for its confirmation with the Channel#WaitForConfirmsOrDie(TimeSpan) method. The method returns as soon as the
                        //message has been confirmed. If the message is not confirmed within the timeout or if it is nack-ed (meaning the broker could not take care of it for
                        //some reason), the method will throw an exception
                        channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, QueueHealthReportChannelExtensions.SecondsToWaitsPublisherConfirms));
                    }
                    catch(Exception err)
                    {
                        return new QueuePublishResult(false, new TimeoutException($"Publisher {GetType().Name} received a NACK or timed-out: {err.Message}", err));
                    }

                    return new QueuePublishResult(true);
                }
                catch (Exception err)
                {
                    _logger.LogInformation($"Publisher {PublisherName} publish error {err}");
                    return new QueuePublishResult(false, err);
                }
            }, cancellationToken);
        }
    }
}
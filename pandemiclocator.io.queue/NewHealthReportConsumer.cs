using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.queue.abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace pandemiclocator.io.queue
{
    public class NewHealthReportConsumer : EventingBasicConsumer, IPandemicEvent
    {
        private readonly HandleNewHealthReportEventCallback _newHealthReportEventCallback;
        private readonly CancellationToken _stoppingToken;
        private readonly ILogger _logger;
        private readonly IModel _channel;

        public NewHealthReportConsumer(HandleNewHealthReportEventCallback newHealthReportEventCallback, CancellationToken stoppingToken, ILogger logger, IModel channel) : base(channel)
        {
            _newHealthReportEventCallback = newHealthReportEventCallback;
            _stoppingToken = stoppingToken;
            _logger = logger;
            _channel = channel;
            channel.InitializeChannelForHealthReport();

            Received += OnReceived;
            Shutdown += OnConsumerShutdown;
            Registered += OnConsumerRegistered;
            Unregistered += OnConsumerUnregistered;
            ConsumerCancelled += OnConsumerConsumerCancelled;
        }

        private void OnReceived(object sender, BasicDeliverEventArgs ea)
        {
            _stoppingToken.ThrowIfCancellationRequested();
            var content = System.Text.Encoding.UTF8.GetString(ea.Body);
            var handleResult = HandleMessage(content);
            if (handleResult.IsSuccess)
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            else
            {
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        }

        private (bool IsSuccess, Exception Error) HandleMessage(string content)
        {
            try
            {
                _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} handled content {content}");
                if (string.IsNullOrEmpty(content))
                {
                    throw new InvalidDataException("A non-empty event content was expected.");
                }

                return _newHealthReportEventCallback(content);
            }
            catch (Exception err)
            {
                _logger.LogInformation($"An unexpected error has occurred during {nameof(NewHealthReportConsumer)}. {err}");
                return (false, new ApplicationException($"An unexpected error has occurred during {nameof(NewHealthReportConsumer)}. {err.Message}", err));
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} cancelled tag {e.ConsumerTag}"); }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} unregistered tag {e.ConsumerTag}"); }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} registered tag {e.ConsumerTag}"); }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} shutdown tag {e.ReplyText}"); }
    }
}

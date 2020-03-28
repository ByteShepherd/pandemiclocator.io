using System;
using System.Collections.Generic;
using System.Text;
using infra.api.pandemiclocator.io.Queue;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace events.pandemiclocator.io
{
    public class NewHealthReportConsumer : EventingBasicConsumer, IPandemicEvent
    {
        private readonly ILogger _logger;
        private readonly IModel _channel;

        public NewHealthReportConsumer(ILogger logger, IModel channel) : base(channel)
        {
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
                //TODO:

                return (true, null);
            }
            catch(Exception err)
            {
                _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} handled error {err.ToString()}");
                return (false, err);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
    }
}

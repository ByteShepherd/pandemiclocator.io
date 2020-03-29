using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.database;
using pandemiclocator.io.database.abstractions;
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
                    throw new InvalidDataException("Este consumidor não aceita eventos com conteúdo vazio.");
                }

                HealthReport contentObject;
                try
                {
                    contentObject = JsonSerializer.Deserialize<HealthReport>(content);
                }
                catch
                {
                    throw new InvalidDataException("Este consumidor não aceita eventos diferentes de HealthReport.");
                }

                if (contentObject == null)
                {
                    throw new InvalidDataException("Este consumidor não recebeu um evento HealthReport serializável.");
                }

                try
                {
                    return _newHealthReportEventCallback(contentObject);
                }
                catch (Exception err)
                {
                    throw new AmazonDynamoDBException($"Este consumidor não conseguiu se conectar ao DynamoDB para gravar o evento de HealthReport. {err.Message}", err);
                }
            }
            catch(Exception err)
            {
                _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} handled error {err}");
                return (false, err);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} cancelled tag {e.ConsumerTag}"); }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} unregistered tag {e.ConsumerTag}"); }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} registered tag {e.ConsumerTag}"); }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { _logger.LogInformation($"Consumer {nameof(NewHealthReportConsumer)} shutdown tag {e.ReplyText}"); }
    }
}

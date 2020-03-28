using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Amazon.DynamoDBv2;
using infra.api.pandemiclocator.io.Database;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.abstractions.Database;
using pandemiclocator.io.abstractions.Queue;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace infra.api.pandemiclocator.io.Queue
{
    public class NewHealthReportConsumer : EventingBasicConsumer, IPandemicEvent
    {
        private readonly IDynamoDbConfiguration _dynamoConfiguration;
        private readonly CancellationToken _stoppingToken;
        private readonly ILogger _logger;
        private readonly IModel _channel;

        public NewHealthReportConsumer(IDynamoDbConfiguration dynamoConfiguration, CancellationToken stoppingToken, ILogger logger, IModel channel) : base(channel)
        {
            _dynamoConfiguration = dynamoConfiguration;
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
                    using (var context = new DynamoDbProvider(_dynamoConfiguration))
                    {
                        try
                        {
                            var dynamoCall = context.SaveAsync(contentObject, _stoppingToken);
                            dynamoCall.Wait(_stoppingToken);
                        }
                        catch (Exception err)
                        {
                            throw new AmazonDynamoDBException($"Este consumidor não conseguiu gravar o evento de HealthReport. {err.Message}", err);
                        }
                    }
                }
                catch (Exception err)
                {
                    throw new AmazonDynamoDBException($"Este consumidor não conseguiu se conectar ao DynamoDB para gravar o evento de HealthReport. {err.Message}", err);
                }

                return (true, null);
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

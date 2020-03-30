using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.database;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.abstractions.Models;
using pandemiclocator.io.queue;
using pandemiclocator.io.queue.abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.pandemiclocator.io.Infra.Services
{
    //Fonte: https://www.c-sharpcorner.com/article/consuming-rabbitmq-messages-in-asp-net-core/
    public class HealthReportConsumerService : BackgroundService
    {
        private readonly IDynamoDbConfiguration _dynamoConfiguration;
        private readonly IHealthReportFactoryProvider _healthReportFactoryProvider;
        private readonly ILogger _logger;

        public HealthReportConsumerService(IDynamoDbConfiguration dynamoConfiguration, ILoggerFactory loggerFactory, IHealthReportFactoryProvider healthReportFactoryProvider)
        {
            _dynamoConfiguration = dynamoConfiguration;
            _healthReportFactoryProvider = healthReportFactoryProvider;
            _logger = loggerFactory.CreateLogger<HealthReportConsumerService>();
        }

        private static CancellationToken _stoppingToken;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new NewHealthReportConsumer(NewHealthReportEventCallback, stoppingToken, _logger, _healthReportFactoryProvider.Channel);

            //When RabbitMQ delivers a message to a consumer, it needs to know when to consider the message to be successfully sent.
            //What kind of logic is optimal depends on the system. It is therefore primarily an application decision. In AMQP 0-9-1 it is
            //made when a consumer is registered using the basic.consume method
            _healthReportFactoryProvider.Channel.BasicConsume(QueueHealthReportChannelExtensions.HealthReportQueueName,
                    QueueHealthReportChannelExtensions.UseAutoAck,
                    consumer);

            return Task.CompletedTask;
        }

        private (bool IsSuccess, Exception Error) NewHealthReportEventCallback(string eventData)
        {
            try
            {
                if (_stoppingToken.IsCancellationRequested)
                {
                    return (false, new ApplicationException($"{nameof(HealthReportConsumerService)} was request to stop."));
                }

                HealthReport contentObject;
                try
                {
                    contentObject = JsonSerializer.Deserialize<HealthReport>(eventData);
                }
                catch
                {
                    return (false, new InvalidDataException("A serializable event of type HealthReport was expected."));
                }

                if (contentObject == null)
                {
                    return (false, new InvalidDataException("A non-empty event of type HealthReport was expected."));
                }

                try
                {
                    using (var context = new DynamoDbProvider(_dynamoConfiguration))
                    {
                        try
                        {
                            var dynamoCall = context.SaveAsync(contentObject, _stoppingToken);
                            dynamoCall.Wait(_stoppingToken);
                            return (true, null);
                        }
                        catch (Exception err)
                        {
                            return (false, new AmazonDynamoDBException($"An error occurred on HealthReport storing. {err.Message}", err));
                        }
                    }
                }
                catch (Exception err)
                {
                    return (false, new AmazonDynamoDBException($"The HealthReport event cannot be saved on unavailable data store. {err.Message}", err));
                }
            }
            catch (Exception err)
            {
                return (false, new ApplicationException($"An unexpected error has occurred during {nameof(NewHealthReportEventCallback)}. {err.Message}", err));
            }
        }

        public override void Dispose()
        {
            _healthReportFactoryProvider?.Dispose();
            base.Dispose();
        }
    }
}

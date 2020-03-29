using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.database;
using pandemiclocator.io.database.abstractions;
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
            _healthReportFactoryProvider.Channel.BasicConsume(QueueHealthReportChannelExtensions.HealthReportQueueName, false, consumer);
            return Task.CompletedTask;
        }

        private (bool IsSuccess, Exception Error) NewHealthReportEventCallback(HealthReport healthReport)
        {
            try
            {
                _stoppingToken.ThrowIfCancellationRequested();
                using (var context = new DynamoDbProvider(_dynamoConfiguration))
                {
                    try
                    {
                        var dynamoCall = context.SaveAsync(healthReport, _stoppingToken);
                        dynamoCall.Wait(_stoppingToken);
                        return (true, null);
                    }
                    catch (Exception err)
                    {
                        return (false, new AmazonDynamoDBException($"Este consumidor não conseguiu gravar o evento de HealthReport. {err.Message}", err));
                    }
                }
            }
            catch (Exception err)
            {
                return (false, new AmazonDynamoDBException($"Este consumidor não conseguiu se conectar ao DynamoDB para gravar o evento de HealthReport. {err.Message}", err));
            }
        }

        public override void Dispose()
        {
            _healthReportFactoryProvider?.Dispose();
            base.Dispose();
        }
    }
}

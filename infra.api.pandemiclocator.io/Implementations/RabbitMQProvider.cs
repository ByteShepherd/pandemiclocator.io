using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using infra.api.pandemiclocator.io.Interfaces;
using infra.api.pandemiclocator.io.Queue;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Implementations
{
    //Fopnte: https://github.com/renatogroffe/RabbitMQ-DotNetCore2.1/tree/master/ExemploRabbitMQ/APIMensagens
    public class RabbitMQProvider : IRabbitMqProvider
    {
        private readonly IRabbitFactoryProvider _rabbitFactoryProvider;

        public RabbitMQProvider(IRabbitFactoryProvider rabbitFactoryProvider)
        {
            _rabbitFactoryProvider = rabbitFactoryProvider;
        }

        public QueueDeclareOk QueueDeclare(string queueName, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            using (var connection = _rabbitFactoryProvider.Factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    return channel.QueueDeclare(queue: queueName,
                        durable: durable,
                        exclusive: exclusive,
                        autoDelete: autoDelete,
                        arguments: arguments);
                }
            }
        }

        public QueuePublishResult Publish<T>(string queueName, T content) where T : class
        {
            try
            {
                using (var connection = _rabbitFactoryProvider.Factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.BasicPublish(exchange: "",
                            routingKey: queueName,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(content)));
                    }
                }

                return new QueuePublishResult(true);
            }
            catch(Exception error)
            {
                return new QueuePublishResult(false, error);
            }
        }
    }
}

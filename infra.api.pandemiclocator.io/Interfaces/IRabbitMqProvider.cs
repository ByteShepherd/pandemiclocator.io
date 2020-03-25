using System.Collections.Generic;
using infra.api.pandemiclocator.io.Queue;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IRabbitMqProvider
    {
        QueueDeclareOk QueueDeclare(string queueName, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null);
        QueuePublishResult Publish<T>(string queueName, T content) where T : class;
    }
}
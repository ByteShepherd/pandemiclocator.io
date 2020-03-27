using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Queue
{
    public static class ChannelExtensions
    {
        public static void InitializeChannelForHealthReport(this IModel channel)
        {
            var exchangeName = $"{IPandemicEvent.DefaultPrefix}exchange";
            var queueName = $"{IPandemicEvent.DefaultPrefix}healthreport";
            var routeKey = $"{queueName}.*";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routeKey, null);
            channel.BasicQos(0, 1, false);
        }
    }
}

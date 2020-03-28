using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Queue
{
    public static class ChannelExtensions
    {
        public static string HealthReportExchangeName = $"{IPandemicEvent.DefaultPrefix}exchange";
        public static string HealthReportQueueName = $"{IPandemicEvent.DefaultPrefix}healthreport";
        public static string HealthReportRouteKey = $"{HealthReportQueueName}.*";

        public static void InitializeChannelForHealthReport(this IModel channel)
        {
            channel.ExchangeDeclare(HealthReportExchangeName, ExchangeType.Topic);
            channel.QueueDeclare(HealthReportQueueName, false, false, false, null);
            channel.QueueBind(HealthReportQueueName, HealthReportExchangeName, HealthReportRouteKey, null);
            channel.BasicQos(0, 1, false);
        }
    }
}

using pandemiclocator.io.queue.abstractions;
using RabbitMQ.Client;

namespace pandemiclocator.io.queue
{
    public static class QueueHealthReportChannelExtensions
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

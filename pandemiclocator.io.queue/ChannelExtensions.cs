using pandemiclocator.io.queue.abstractions;
using RabbitMQ.Client;

namespace pandemiclocator.io.queue
{
    //Source of information: https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
    public static class QueueHealthReportChannelExtensions
    {
        public static string HealthReportExchangeName = $"{IPandemicEvent.DefaultPrefix}exchange";
        public static string HealthReportQueueName = $"{IPandemicEvent.DefaultPrefix}healthreport";
        public static string HealthReportRouteKey = $"{HealthReportQueueName}.*";

        //When RabbitMQ quits or crashes it will forget the queues and messages unless you tell it not to. Two things are required to
        //make sure that messages aren't lost: we need to mark both the queue and messages as durable.
        public const bool UseDurableQueues = true;
        
        public const bool UseAutoAck = false;

        public const int SecondsToWaitsPublisherConfirms = 5;

        public static void InitializeChannelForHealthReport(this IModel channel)
        {
            //Publisher confirms are a RabbitMQ extension to the AMQP 0.9.1 protocol, so they are not enabled by default.
            //Publisher confirms are enabled at the channel level with the ConfirmSelect method. This method must be called on
            //every channel that you expect to use publisher confirms. Confirms should be enabled just once, not for every message published.
            channel.ConfirmSelect();

            channel.ExchangeDeclare(HealthReportExchangeName, ExchangeType.Topic);
            channel.QueueDeclare(HealthReportQueueName, UseDurableQueues, false, false, null);
            channel.QueueBind(HealthReportQueueName, HealthReportExchangeName, HealthReportRouteKey, null);

            //PrefetchCount = 1 setting. This tells RabbitMQ not to give more than one message to a worker at a time.
            //Or, in other words, don't dispatch a new message to a worker until it has processed and acknowledged the previous one.
            //Instead, it will dispatch it to the next worker that is not still busy. 
            channel.BasicQos(0, 1, false);
        }
    }
}

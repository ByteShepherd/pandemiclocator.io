using System;
using RabbitMQ.Client;

namespace pandemiclocator.io.queue.abstractions
{
    public interface IQueueFactoryProvider : IDisposable
    {
        ConnectionFactory Factory { get; }
        IConnection Connection { get; }
        IModel Channel { get; }
        IBasicProperties ChannelBasicProperties { get; }
    }
}
using System;
using RabbitMQ.Client;

namespace pandemiclocator.io.abstractions.Queue
{
    public interface IQueueFactoryProvider : IDisposable
    {
        ConnectionFactory Factory { get; }
        IConnection Connection { get; }
        IModel Channel { get; }
    }
}
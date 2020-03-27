using System;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IRabbitFactoryProvider : IDisposable
    {
        ConnectionFactory Factory { get; }
        IConnection Connection { get; }
        IModel Channel { get; }
    }
}
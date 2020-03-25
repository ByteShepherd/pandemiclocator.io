using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IRabbitFactoryProvider
    {
        ConnectionFactory Factory { get; }
    }
}
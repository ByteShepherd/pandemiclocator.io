using infra.api.pandemiclocator.io.Queue;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IEventingBasicPublisher<T> where T : class
    {
        QueuePublishResult Publish(T message);
    }
}
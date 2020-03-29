namespace pandemiclocator.io.queue.abstractions
{
    public interface IEventingBasicPublisher<T> where T : class
    {
        QueuePublishResult Publish(T message);
    }
}
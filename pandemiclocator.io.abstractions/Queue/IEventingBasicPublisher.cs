namespace pandemiclocator.io.abstractions.Queue
{
    public interface IEventingBasicPublisher<T> where T : class
    {
        QueuePublishResult Publish(T message);
    }
}
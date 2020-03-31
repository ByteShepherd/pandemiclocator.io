using System.Threading;
using System.Threading.Tasks;

namespace pandemiclocator.io.queue.abstractions
{
    public interface IEventingBasicPublisher<T> where T : class
    {
        Task<QueuePublishResult> PublishAsync(T message, CancellationToken cancellationToken);
    }
}
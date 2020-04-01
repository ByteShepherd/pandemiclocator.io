using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.model.abstractions;

namespace pandemiclocator.io.queue.abstractions
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
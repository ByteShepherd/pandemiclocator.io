using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.queue.abstractions
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
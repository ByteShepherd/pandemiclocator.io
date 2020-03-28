using pandemiclocator.io.abstractions.Database;

namespace pandemiclocator.io.abstractions.Queue
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
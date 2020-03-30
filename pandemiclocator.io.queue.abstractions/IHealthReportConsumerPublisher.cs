using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.abstractions.Models;

namespace pandemiclocator.io.queue.abstractions
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
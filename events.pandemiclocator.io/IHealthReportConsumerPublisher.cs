using events.pandemiclocator.io.Content;
using infra.api.pandemiclocator.io.Interfaces;

namespace events.pandemiclocator.io
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
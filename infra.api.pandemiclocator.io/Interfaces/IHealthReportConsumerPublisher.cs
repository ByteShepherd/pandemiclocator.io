using infra.api.pandemiclocator.io.Data.Tables;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IHealthReportConsumerPublisher : IEventingBasicPublisher<HealthReport>
    {

    }
}
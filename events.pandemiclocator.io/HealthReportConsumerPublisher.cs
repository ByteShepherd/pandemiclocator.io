using events.pandemiclocator.io.Content;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.Extensions.Logging;

namespace events.pandemiclocator.io
{
    public class HealthReportConsumerPublisher : EventingBasicPublisher<HealthReport>, IHealthReportConsumerPublisher
    {
        public HealthReportConsumerPublisher(ILogger logger, IHealthReportFactoryProvider healthReportFactoryProvider) : base(logger, healthReportFactoryProvider)
        {
        }

        protected override string PublisherName => nameof(HealthReportConsumerPublisher);
    }
}
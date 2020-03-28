using infra.api.pandemiclocator.io.Data.Tables;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.Extensions.Logging;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class HealthReportConsumerPublisher : EventingBasicPublisher<HealthReport>, IHealthReportConsumerPublisher
    {
        public HealthReportConsumerPublisher(ILoggerFactory loggerFactory, IHealthReportFactoryProvider healthReportFactoryProvider) 
            : base(loggerFactory, healthReportFactoryProvider)
        {
        }

        protected override string PublisherName => nameof(HealthReportConsumerPublisher);
        protected override ILogger InitializeLogger()
        {
            return LoggerFactory.CreateLogger<HealthReportConsumerPublisher>();
        }
    }
}
using Microsoft.Extensions.Logging;
using pandemiclocator.io.abstractions.Database;
using pandemiclocator.io.abstractions.Queue;

namespace infra.api.pandemiclocator.io.Queue
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
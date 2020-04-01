using Microsoft.Extensions.Logging;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.model.abstractions;
using pandemiclocator.io.queue.abstractions;

namespace pandemiclocator.io.queue
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
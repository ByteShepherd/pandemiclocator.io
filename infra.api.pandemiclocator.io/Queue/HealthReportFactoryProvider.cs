using pandemiclocator.io.abstractions.Queue;

namespace infra.api.pandemiclocator.io.Queue
{
    public class HealthReportFactoryProvider : RabbitFactoryProvider, IHealthReportFactoryProvider
    {
        public HealthReportFactoryProvider(IQueueConnectionSection queueConnectionSection) : base(queueConnectionSection)
        {
        }

        protected override void InitializeChannel()
        {
            Channel.InitializeChannelForHealthReport();
        }
    }
}
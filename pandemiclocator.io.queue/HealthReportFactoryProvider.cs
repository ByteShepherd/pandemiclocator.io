using pandemiclocator.io.queue.abstractions;

namespace pandemiclocator.io.queue
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
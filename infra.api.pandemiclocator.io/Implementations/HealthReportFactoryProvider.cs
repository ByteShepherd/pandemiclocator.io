using infra.api.pandemiclocator.io.Interfaces;
using infra.api.pandemiclocator.io.Queue;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class HealthReportFactoryProvider : RabbitFactoryProvider
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
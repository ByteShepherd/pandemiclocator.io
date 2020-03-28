

using pandemiclocator.io.abstractions.Queue;

namespace infra.api.pandemiclocator.io.Queue
{
    public class QueueConnectionSection : IQueueConnectionSection
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
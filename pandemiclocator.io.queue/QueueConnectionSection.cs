

using pandemiclocator.io.queue.abstractions;

namespace pandemiclocator.io.queue
{
    public class QueueConnectionSection : IQueueConnectionSection
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
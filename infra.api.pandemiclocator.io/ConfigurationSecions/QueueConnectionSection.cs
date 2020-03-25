using infra.api.pandemiclocator.io.Interfaces;

namespace infra.api.pandemiclocator.io.ConfigurationSecions
{
    public class QueueConnectionSection : IQueueConnectionSection
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
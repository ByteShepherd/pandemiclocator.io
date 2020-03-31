namespace pandemiclocator.io.queue.abstractions
{
    public interface IQueueConnectionSection
    {
        string HostName { get; set; }
        int Port { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
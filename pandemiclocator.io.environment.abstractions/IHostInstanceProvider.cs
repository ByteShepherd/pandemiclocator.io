namespace pandemiclocator.io.environment.abstractions
{
    public interface IHostInstanceProvider
    {
        bool IsRunningOnCloud { get; }
        string HostInstanceId { get; }
    }
}
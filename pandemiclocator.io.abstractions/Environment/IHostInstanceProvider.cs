namespace pandemiclocator.io.abstractions.Environment
{
    public interface IHostInstanceProvider
    {
        bool IsRunningOnCloud { get; }
        string HostInstanceId { get; }
    }
}
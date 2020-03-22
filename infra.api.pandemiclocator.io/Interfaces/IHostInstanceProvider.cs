namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IHostInstanceProvider
    {
        bool IsRunningOnCloud { get; }
        string HostInstanceId { get; }
    }
}
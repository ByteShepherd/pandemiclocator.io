using System;
using System.Threading;
using System.Threading.Tasks;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IPandemicContext : IDisposable
    {
        Task SaveAsync<T>(T document, CancellationToken cancellationToken);
    }
}
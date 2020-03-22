using System;
using System.Threading;
using System.Threading.Tasks;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IPandemicContext : IDisposable
    {
        Task<T> GetByIdAsync<T>(string key, CancellationToken cancellationToken);
        Task SaveAsync<T>(T document, CancellationToken cancellationToken);
    }
}
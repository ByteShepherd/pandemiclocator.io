using System.Threading;
using System.Threading.Tasks;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IPandemicCache
    {
        Task<bool> SetCacheAsync<T>(string key, T item, CancellationToken cancellationToken) where T : class;
        Task<T> GetCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class;
    }
}
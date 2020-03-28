using System;
using System.Threading;
using System.Threading.Tasks;

namespace pandemiclocator.io.abstractions.Database
{
    public interface IDynamoDbProvider : IDisposable
    {
        Task<T> GetByIdAsync<T>(string key, CancellationToken cancellationToken);
        Task SaveAsync<T>(T document, CancellationToken cancellationToken);
    }
}
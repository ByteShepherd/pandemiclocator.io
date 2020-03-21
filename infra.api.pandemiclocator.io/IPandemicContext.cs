using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Amazon.DynamoDBv2.DataModel;

namespace infra.api.pandemiclocator.io
{
    public interface IPandemicContext : IDisposable
    {
        Task SaveAsync<T>(T document, CancellationToken cancellationToken);
    }
}
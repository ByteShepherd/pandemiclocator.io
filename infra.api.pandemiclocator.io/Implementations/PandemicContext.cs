using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using infra.api.pandemiclocator.io.Interfaces;
using infra.api.pandemiclocator.io.Providers;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class PandemicContext : IPandemicContext
    {
        private AmazonDynamoDBClient _client;
        private DynamoDBContext _context;
        
        public Task SaveAsync<T>(T document, CancellationToken cancellationToken)
        {
            return _context.SaveAsync(document, cancellationToken);
        }

        public PandemicContext()
        {
            _client = DynamoProvider.ClientProvider();
            _context = DynamoProvider.Contextprovider(_client);
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;

            _context?.Dispose();
            _context = null;
        }
    }
}
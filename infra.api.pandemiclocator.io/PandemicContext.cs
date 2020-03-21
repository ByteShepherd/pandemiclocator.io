using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace infra.api.pandemiclocator.io
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
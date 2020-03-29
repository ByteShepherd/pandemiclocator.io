using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public class DynamoDbProvider : IDynamoDbProvider
    {
        public DynamoDbProvider(IDynamoDbConfiguration config)
        {
            _client = DynamoProvider.ClientProvider(config);
            _context = DynamoProvider.Contextprovider(_client);
        }

        private AmazonDynamoDBClient _client;
        private DynamoDBContext _context;
        
        public Task<T> GetByIdAsync<T>(string key, CancellationToken cancellationToken)
        {
            return _context.LoadAsync<T>(key, cancellationToken);
        }

        public Task SaveAsync<T>(T document, CancellationToken cancellationToken)
        {
            return _context.SaveAsync(document, cancellationToken);
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
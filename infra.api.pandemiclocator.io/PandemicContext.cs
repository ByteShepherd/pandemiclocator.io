using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace infra.api.pandemiclocator.io
{
    public class PandemicContext : IDisposable
    {
        private AmazonDynamoDBClient _client;
        public DynamoDBContext Context { get; private set; }

        public PandemicContext()
        {
            _client = DynamoProvider.ClientProvider();
            Context = DynamoProvider.Contextprovider(_client);
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
            
            Context?.Dispose();
            Context = null;
        }
    }
}
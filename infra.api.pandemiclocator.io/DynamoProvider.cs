using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace infra.api.pandemiclocator.io
{
    public class DynamoProvider
    {
        public static BasicAWSCredentials CredentialProvider()
        {
            return new BasicAWSCredentials("AKIAR5NZT37FDFLXKF6O", "6O7ugVW1BfPI0K7nfu16gMH7nHN63QUEAcv4I3wU");
        }

        public static AmazonDynamoDBClient ClientProvider()
        {
            var credentials = DynamoProvider.CredentialProvider();
            return new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        }

        public static AmazonDynamoDBClient ClientProvider(BasicAWSCredentials credentials)
        {
            return new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        }

        public static DynamoDBContext Contextprovider(AmazonDynamoDBClient client)
        {
            return new DynamoDBContext(client);
        }
    }
}

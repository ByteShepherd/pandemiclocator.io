using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public class DynamoDbConfiguration : IDynamoDbConfiguration
    {
        private static BasicAWSCredentials _credentials;
        private static AmazonDynamoDBConfig _dynamoConfiguration;

        public DynamoDbConfiguration(DynamoDbConnectionSection section)
        {
            _credentials = new BasicAWSCredentials(section.AccessKey, section.SecretKey);
            _dynamoConfiguration = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(section.System)
            };
        }


        public BasicAWSCredentials Credentials => _credentials;
        public AmazonDynamoDBConfig DynamoConfiguration => _dynamoConfiguration;
    }
}
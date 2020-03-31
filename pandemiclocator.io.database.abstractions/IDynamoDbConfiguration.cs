using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace pandemiclocator.io.database.abstractions
{
    public interface IDynamoDbConfiguration
    {
        BasicAWSCredentials Credentials { get; }
        AmazonDynamoDBConfig DynamoConfiguration { get; }
    }
}
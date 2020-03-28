using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace pandemiclocator.io.abstractions.Database
{
    public interface IDynamoDbConfiguration
    {
        BasicAWSCredentials Credentials { get; }
        AmazonDynamoDBConfig DynamoConfiguration { get; }
    }
}
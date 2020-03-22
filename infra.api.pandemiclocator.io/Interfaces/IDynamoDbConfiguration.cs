using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IDynamoDbConfiguration
    {
        BasicAWSCredentials Credentials { get; }
        AmazonDynamoDBConfig DynamoConfiguration { get; }
    }
}
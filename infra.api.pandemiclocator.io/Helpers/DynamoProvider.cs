﻿using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using infra.api.pandemiclocator.io.Interfaces;

namespace infra.api.pandemiclocator.io.Helpers
{
    public static class DynamoProvider
    {
        public static AmazonDynamoDBClient ClientProvider(IDynamoDbConfiguration config)
        {
            return new AmazonDynamoDBClient(config.Credentials, config.DynamoConfiguration);
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

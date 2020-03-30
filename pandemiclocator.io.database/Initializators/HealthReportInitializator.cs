using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.abstractions.Models;

namespace pandemiclocator.io.database.Initializators
{
    public class HealthReportInitializator
    {
        public static CreateTableRequest InitializeTable()
        {
            return new CreateTableRequest
            {
                TableName = nameof(HealthReport),
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 3,
                    WriteCapacityUnits = 1
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = nameof(HealthReport.Id),
                        KeyType = KeyType.HASH
                    }
                },
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = nameof(HealthReport.Id),
                        AttributeType = ScalarAttributeType.S
                    }
                }
            };
        }
    }
}

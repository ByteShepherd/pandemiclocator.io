using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using api.pandemiclocator.io.Infra.Data.Documents;
using api.pandemiclocator.io.Infra.Data.Tables;

namespace api.pandemiclocator.io.Infra.Initializators
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

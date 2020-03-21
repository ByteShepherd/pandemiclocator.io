using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using models.pandemiclocator.io.Tables;

namespace api.pandemiclocator.io.Infra.Initializators
{
    public class PandemicInitializator
    {
        public static CreateTableRequest InitializeTable()
        {
            return new CreateTableRequest
            {
                TableName = PandemicTable.TableName,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 3,
                    WriteCapacityUnits = 1
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = nameof(PandemicTable.id),
                        KeyType = KeyType.HASH
                    }
                },
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = nameof(PandemicTable.id),
                        AttributeType = ScalarAttributeType.S
                    }
                }
            };
        }
    }
}

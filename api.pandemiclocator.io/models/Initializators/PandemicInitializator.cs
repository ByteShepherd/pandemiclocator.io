using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using models.pandemiclocator.io;

namespace api.pandemiclocator.io.models.Initializators
{
    public class PandemicInitializator
    {
        public static CreateTableRequest InitializeTable()
        {
            return new CreateTableRequest
            {
                TableName = Pandemic.TableName,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 3,
                    WriteCapacityUnits = 1
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = nameof(Pandemic.id),
                        KeyType = KeyType.HASH
                    }
                },
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = nameof(Pandemic.id),
                        AttributeType = ScalarAttributeType.S
                    }
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using models.pandemiclocator.io;

namespace api.pandemiclocator.io.models.Initializators
{
    public class DynamoInitializator
    {
        public static void Initialize()
        {
            var credentials = new BasicAWSCredentials("AKIAR5NZT37FDFLXKF6O", "6O7ugVW1BfPI0K7nfu16gMH7nHN63QUEAcv4I3wU");
            using var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
            var tableResponse = client.ListTablesAsync().GetAwaiter().GetResult();
            if (!tableResponse.TableNames.Contains(Pandemic.TableName))
            {
                client.CreateTableAsync(PandemicInitializator.InitializeTable()).GetAwaiter().GetResult();
            }
        }
    }
}

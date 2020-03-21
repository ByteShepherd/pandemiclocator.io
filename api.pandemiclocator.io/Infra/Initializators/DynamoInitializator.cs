using infra.api.pandemiclocator.io;
using models.pandemiclocator.io.Tables;

namespace api.pandemiclocator.io.Infra.Initializators
{
    public class DynamoInitializator
    {
        public static void Initialize()
        {
            using (var client = DynamoProvider.ClientProvider())
            {
                var tableResponse = client.ListTablesAsync().GetAwaiter().GetResult();
                if (!tableResponse.TableNames.Contains(PandemicTable.TableName))
                {
                    client.CreateTableAsync(PandemicInitializator.InitializeTable()).GetAwaiter().GetResult();
                }
            }
        }
    }
}

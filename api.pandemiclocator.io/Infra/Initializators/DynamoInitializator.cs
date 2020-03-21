using api.pandemiclocator.io.Infra.Data.Tables;
using infra.api.pandemiclocator.io;

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

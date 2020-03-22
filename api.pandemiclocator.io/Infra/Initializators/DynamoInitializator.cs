using System.Threading;
using api.pandemiclocator.io.Infra.Data.Documents;
using api.pandemiclocator.io.Infra.Data.Tables;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Providers;

namespace api.pandemiclocator.io.Infra.Initializators
{
    public class DynamoInitializator
    {
        public static void Initialize()
        {
            using (var client = DynamoProvider.ClientProvider())
            {
                var tableResponse = client.ListTablesAsync().GetAwaiter().GetResult();
                if (!tableResponse.TableNames.Contains(nameof(HealthReport)))
                {
                    client.CreateTableAsync(HealthReportInitializator.InitializeTable()).GetAwaiter().GetResult();

                    //TODO: PARA CHECAR SE TABELA CRIADA E ATIVA
                    //bool isTableAvailable = false;
                    //while (!isTableAvailable)
                    //{
                    //    Thread.Sleep(5000);
                    //    var tableStatus = client.DescribeTableAsync(PandemicTable.TableName).GetAwaiter().GetResult();
                    //    isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                    //}
                }
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Database;
using pandemiclocator.io.abstractions;
using pandemiclocator.io.abstractions.Database;

namespace api.pandemiclocator.io.Infra.Initializators
{
    public class DynamoInitializator
    {
        public static async Task Initialize(IDynamoDbConfiguration config, CancellationToken cancellationToken)
        {
            using (var client = DynamoProvider.ClientProvider(config))
            {
                var tableResponse = await client.ListTablesAsync(cancellationToken);
                if (!tableResponse.TableNames.Contains(nameof(HealthReport)))
                {
                    await client.CreateTableAsync(HealthReportInitializator.InitializeTable(), cancellationToken);

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

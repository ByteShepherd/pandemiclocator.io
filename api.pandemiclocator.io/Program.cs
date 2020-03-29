using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.Initializators;

namespace api.pandemiclocator.io
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                using (var scope = host.Services.CreateScope())
                {
                    var config = scope.ServiceProvider.GetRequiredService<IDynamoDbConfiguration>();
                    await DynamoInitializator.Initialize(config, CancellationToken.None);
                }

                await host.RunAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();

                    //TODO: Review for production
                });
    }
}

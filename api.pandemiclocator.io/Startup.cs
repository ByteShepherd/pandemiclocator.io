using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using api.pandemiclocator.io.Infra.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.cache;
using pandemiclocator.io.cache.abstractions;
using pandemiclocator.io.database;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.environment;
using pandemiclocator.io.environment.abstractions;
using pandemiclocator.io.queue;
using pandemiclocator.io.queue.abstractions;
using pandemiclocator.io.services;
using pandemiclocator.io.services.abstractions;
using Formatting = System.Xml.Formatting;

namespace api.pandemiclocator.io
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        const string CorsPolicy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //########## IDIOMAS
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US")
                };
            });

            //############### CONTROLLERS
            services
                .AddControllers(options =>
                {
                    options.AllowEmptyInputInBodyModelBinding = true;
                    options.EnableEndpointRouting = true;
                    options.SuppressAsyncSuffixInActionNames = true;

                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.WriteIndented = false;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                });

            //############### CORS
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    corsBuilder =>
                    {
                        corsBuilder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            //################# REDIS
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "pandemic:";
            });


            //############### COMPRESSION
            services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                });

            //###### PANDEMIC
            ConfigurePandemicServices(services);

            //###### PANDEMIC QUEUE
            ConfigurePandemicQueueServices(services);
        }

        private void ConfigurePandemicServices(IServiceCollection services)
        {
            services.AddTransient<IDbPandemicConnection, DbPandemicConnection>((serviceProvider) =>
            {
                var connString = Configuration.GetConnectionString("PandemicDatabase");
                return new DbPandemicConnection(connString);
            });

            services.AddTransient<IGeolocationService, GeolocationService>((serviceProvider) =>
            {
                var conn = serviceProvider.GetService<IDbPandemicConnection>();
                return new GeolocationService(conn);
            });

            services.AddScoped<IRedisProvider, RedisProvider>();
            services.AddSingleton<IHostInstanceProvider, HostInstanceProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        private void ConfigurePandemicQueueServices(IServiceCollection services)
        {
            services.AddSingleton<IHealthReportFactoryProvider, HealthReportFactoryProvider>();
            services.AddSingleton<IQueueConnectionSection, QueueConnectionSection>((serviceProvider) =>
            {
                var section = new QueueConnectionSection();
                Configuration.GetSection("QueueConnection").Bind(section);
                return section;
            });

            services.AddHostedService<HealthReportConsumerService>();
            services.AddSingleton<IHealthReportConsumerPublisher, HealthReportConsumerPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(CorsPolicy)
                .UseRequestLocalization()
                /*TODO:.UseExceptionHandler()*/;

            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopping.Register(OnShuttingdown);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        public virtual void OnStarted()
        {
            //this code is called when the application starts
        }

        public virtual void OnShuttingdown()
        {
            //this code is called when the application starts
        }

        public virtual void OnStopped()
        {
            //this code is called when the application stops
        }
    }
}

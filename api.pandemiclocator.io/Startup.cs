using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Internal;
using Amazon.Runtime;
using api.pandemiclocator.io.Infra.Initializators;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.ConfigurationSecions;
using infra.api.pandemiclocator.io.Implementations;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        }

        private void ConfigurePandemicServices(IServiceCollection services)
        {
            services.AddScoped<IDynamoDbProvider, DynamoDbProvider>();
            services.AddScoped<IRedisProvider, RedisProvider>();

            services.AddSingleton<IDynamoDbConfiguration, DynamoDbConfiguration>((serviceProvider) =>
            {
                var section = new DynamoDbConnectionSection();
                Configuration.GetSection("DynamoDbConnection").Bind(section);
                return new DynamoDbConfiguration(section);
            });

            services.AddSingleton<IHostInstanceProvider, HostInstanceProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
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

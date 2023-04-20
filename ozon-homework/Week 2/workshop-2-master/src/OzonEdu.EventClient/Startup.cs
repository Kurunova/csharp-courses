using System;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OzonEdu.EventClient.Extensions;
using OzonEdu.EventClient.Middleware;
using OzonEdu.EventClient.Options;
using OzonEdu.EventClient.Services;

namespace OzonEdu.EventClient
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<StoreConfig>(_configuration.GetSection("StoreConfig"));

            services.AddScoped<DemoInterceptor>();
            services.AddGrpcClient<Ozon.EventGenerator.Generator.GeneratorClient>(
                options =>
                {
                    options.Address = new Uri("https://localhost:7235/");
                }).AddInterceptor<DemoInterceptor>();

            services.AddSomeServices();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IDemoService service)
        {
            logger.LogInformation("Start configure {Env}", service.Env);

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
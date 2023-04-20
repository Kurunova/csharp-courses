using Microsoft.Extensions.DependencyInjection;
using OzonEdu.EventClient.BL;
using OzonEdu.EventClient.HostedServices;
using OzonEdu.EventClient.Services;

namespace OzonEdu.EventClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSomeServices(this IServiceCollection services)
        {
            services.AddScoped<IDemoService, DemoService>();
            services.AddHostedService<SomeHostedService>();
            services.AddSingleton<IEventStorage, EventStorage>();

            return services;
        }
    }
}
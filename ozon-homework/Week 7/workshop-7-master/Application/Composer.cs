using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace NanoPaymentSystem.Application;

public static class Composer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Composer));
        return services;
    }
}
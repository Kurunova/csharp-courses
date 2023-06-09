﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.Repository;

namespace NanoPaymentSystem.Database;

public static class Composer
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("DbConnection");
        services.AddSingleton<IPaymentRepository>(new PaymentRepository(dbConnectionString));
        return services;
    }
}

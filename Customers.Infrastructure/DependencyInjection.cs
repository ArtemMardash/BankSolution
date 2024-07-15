using Customers.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Customers.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastracture(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Customers.Application.DependencyInjection)));
    }

    public static IServiceCollection RegisterRabbitMq(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = "localhost";
        });
        services.AddScoped<IBrokerPublisher, BrokerPublisher>();
        services.AddMassTransit(x => { x.UsingRabbitMq(); });
        return services;
    }
}
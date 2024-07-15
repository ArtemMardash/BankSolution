using Accounts.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterRabitMq(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CustomerCreatedConsumer>();
            x.AddConsumer<CustomerDeletedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("CustomerCreated",
                        s =>
                            s.ConfigureConsumer<CustomerCreatedConsumer>(context));
                    cfg.ReceiveEndpoint("CustomerDeleted",
                        s =>
                            s.ConfigureConsumer<CustomerDeletedConsumer>(context));
                }
            );
        });
    }
}
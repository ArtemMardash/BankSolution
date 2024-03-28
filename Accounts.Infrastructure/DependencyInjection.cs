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
            x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("CustomerCreated",
                        s =>
                            s.ConfigureConsumer<CustomerCreatedConsumer>(context));
                }
            );
        });
    }
}
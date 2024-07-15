using Customers.Application.Interfaces;
using Customers.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<CustomerDbContext>(opt =>
            {
                opt.UseNpgsql("Host=localhost;Port=5432;Database=Customers;Username=postgres;Password=postgres",
                    builder => builder.MigrationsAssembly(typeof(CustomerDbContext).Assembly.GetName().Name));
            }
        );
        services.AddScoped<OutboxRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
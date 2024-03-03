using Accounts.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AccountDbContext>(opt =>
            {
                opt.UseNpgsql("Host=localhost;Port=5432;Database=Accounts;Username=postgres;Password=postgres",
                    builder => builder.MigrationsAssembly(typeof(AccountDbContext).Assembly.GetName().Name));
            }
        );
        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<UnitOfWork>();
    }
}
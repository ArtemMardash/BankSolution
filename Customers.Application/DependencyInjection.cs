using Microsoft.Extensions.DependencyInjection;

namespace Customers.Application;

public static class DependencyInjection
{
    public static void RegisterUserCases(this IServiceCollection services)
    {
        // services.AddScoped<IGetCustomerUseCase, GetCustomerUseCase>();
        // services.AddScoped<IAddCustomerUseCase, AddCustomerUseCase>();
        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
    }
}
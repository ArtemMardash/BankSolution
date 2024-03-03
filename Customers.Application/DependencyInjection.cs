using Customers.Application.UseCases;
using Customers.Application.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Application;

public static class DependencyInjection
{
    public static void RegisterUserCases(this IServiceCollection services)
    {
        services.AddScoped<IGetCustomerUseCase, GetCustomerUseCase>();
        services.AddScoped<IAddCustomerUseCase, AddCustomerUseCase>();
    }
}
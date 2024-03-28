using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Application;

public static class DependencyInjection
{
    public static void RegisterUseCases(this IServiceCollection services)
    {
        // services.AddScoped<IGetCustomerAccountsUseCase, GetCustomerAccountsUseCase>();
        // services.AddScoped<ICreateAccountUseCase, CreateAccountUseCase>();
        // services.AddScoped<IWithdrawUseCase, WithdrawUseCase>();
        // services.AddScoped<IChangeStatusUseCase, ChangeStatusUseCase>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
    }
}
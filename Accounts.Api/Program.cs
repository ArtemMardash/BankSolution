using Accounts.Application;
using Accounts.Application.Dtos;
using Accounts.Infrastructure;
using Accounts.Persistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence();
builder.Services.RegisterUseCases();
builder.Services.RegisterRabitMq();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetService<UnitOfWork>();
    context?.Migrate();
}


//Create new Account
app.MapPost("/api/accounts/create",
        async (CreateAccountDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(dto, cancellationToken);
        })
    .WithName("CreateNewAccount")
    .WithTags("Accounts")
    .WithOpenApi();

//Method to withdraw
app.MapPut("/api/accounts/withdraw",
        async (WithdrawDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(dto, cancellationToken);
        })
    .WithName("Withdraw")
    .WithTags("Accounts")
    .WithOpenApi();

//Method to make a deposit
app.MapPut("/api/accounts/deposit",
        async (DepositDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(dto, cancellationToken);
        })
    .WithName("Deposit")
    .WithTags("Accounts")
    .WithOpenApi();

app.MapGet("/api/accounts/{customerId}/list/",
        async (Guid customerId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var accounts =
                await mediator.Send(new GetCustomerAccountsDto { CustomerId = customerId }, cancellationToken);
            return accounts;
        })
    .WithName("GetCustomerAccountsList")
    .WithTags("Accounts")
    .WithOpenApi();
app.MapPut("/api/accounts/changeStatus/",
        async (ChangeStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(dto, cancellationToken);
        })
    .WithName("ChangeStatus")
    .WithTags("Accounts")
    .WithOpenApi();
app.Run();

namespace Accounts.Api
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
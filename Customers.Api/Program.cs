using Customers.Application;
using Customers.Application.Dtos;
using Customers.Infrastructure;
using Customers.Persistence;
using Customers.Persistence.AppContext;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterUserCases();
builder.Services.AddPersistence();
builder.Services.AddInfrastracture();
builder.Services.RegisterRabbitMq();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetService<CustomerDbContext>();
    context?.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/customers", async (IMediator mediator, CancellationToken cancellationToken) =>
{
    return await mediator.Send(new GetCustomersRequest(), cancellationToken);
});

app.MapGet("/api/customer/{id:guid}",
        async (Guid id,IMediator mediator, CancellationToken cancellationToken) =>
        {
            return await mediator.Send(new GetCustomerRequest{Id = id}, cancellationToken);
        })
    .WithName("Customers")
    .WithOpenApi();

app.MapPost("/api/customer", async ([FromBody]CreateCustomerRequest dto, IMediator mediator, CancellationToken cancellationToken) =>
{
    return await mediator.Send(dto, cancellationToken);
});

app.MapDelete("/api/customer/{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
{
    await mediator.Send(new DeleteCustomerRequest { Id = id }, cancellationToken);
});

app.Run();


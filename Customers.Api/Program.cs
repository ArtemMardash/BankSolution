using Customers.Application;
using Customers.Application.Dtos;
using Customers.Application.Dtos.Responses;
using Customers.Application.UseCases;
using Customers.Infrastructure;
using Customers.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterUserCases();
builder.Services.AddPersistence();
builder.Services.AddInfrastracture();
builder.Services.RegisterRabitMq();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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
app.Run();


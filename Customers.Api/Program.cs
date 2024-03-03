using Customers.Application;
using Customers.Application.Dto_s;
using Customers.Application.UseCases;
using Customers.Application.UseCases.Interfaces;
using Customers.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterUserCases();
builder.Services.AddPersistence();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/api/customer/{id}",
        async ([FromRoute]Guid id, [FromServices] IGetCustomerUseCase useCase, CancellationToken cancellationToken) =>
        {
            var customer = await useCase.ExecuteAsync(id, cancellationToken);
            return Results.Ok(customer);
        })
    .WithName("Customers")
    .WithOpenApi();

app.MapPost("/api/customer", async (CreateCustomerDto dto, IAddCustomerUseCase usecase, CancellationToken ct) =>
{
    var id = await usecase.ExcexuteAsync(dto, ct);
    return Results.Ok(id);
});
app.Run();


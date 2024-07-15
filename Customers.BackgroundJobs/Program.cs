using Customers.BackgroundJobs;
using Customers.Infrastructure;
using Customers.Persistence;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddInfrastracture();
builder.Services.RegisterRabbitMq();
builder.Services.AddPersistence();

var host = builder.Build();
host.Run();
using System.Text.Json.Serialization;
using Customers.Application.Interfaces;
using Customers.Persistence;
using Customers.Persistence.Enums;
using MediatR;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace Customers.BackgroundJobs;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;


    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var outboxRepository = scope.ServiceProvider.GetRequiredService<OutboxRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var messages = await outboxRepository.GetNotProccedAsync(stoppingToken);
            foreach (var message in messages)
            {
                var publishEvent = Activator.CreateInstance("Customers.Domain", message.MessageType).Unwrap();
                var payLoad =JsonConvert.DeserializeObject(message.PayLoad, publishEvent.GetType());
                await mediator.Publish(payLoad);
                message.Status = OutboxStatus.Published;
            }

            await unitOfWork.SaveChangesAsync(stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
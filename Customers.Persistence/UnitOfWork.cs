using System.Text.Json;
using System.Text.Json.Serialization;
using Customers.Application.Interfaces;
using Customers.Persistence.AppContext;
using Customers.Persistence.Entities;
using Customers.Persistence.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Customers.Persistence;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly CustomerDbContext _context;
    private readonly IMediator _mediator;


    public UnitOfWork(CustomerDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }


    public void Dispose()
    {
        _context.Dispose();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
        var entities = _context.ChangeTracker
            .Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();
        var domainEvents = entities.SelectMany(e => e.DomainEvents);
        entities.ForEach(e => e.DomainEvents.Clear());
        foreach (var domainEvent in domainEvents)
        {
            _mediator.Publish(domainEvent).GetAwaiter().GetResult();
        }
    }

    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        var entities = _context.ChangeTracker
            .Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();
        var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();
        entities.ForEach(e => e.DomainEvents.Clear());
        await _context.Database.BeginTransactionAsync(cancellationToken);
        foreach (var domainEvent in domainEvents)
        {
            _context.OutboxMessages.Add(new Outbox
            {
                Id = Guid.NewGuid(),
                Status = OutboxStatus.Pending,
                PayLoad = JsonConvert.SerializeObject(domainEvent),
                MessageType = domainEvent.GetType().ToString()
            });
        }
        await _context.Database.CommitTransactionAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
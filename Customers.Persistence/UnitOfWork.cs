using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Persistence.AppContext;
using MediatR;

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
            .Entries<BaseEntity>()
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

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
        var entities = _context.ChangeTracker
            .Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();
        var domainEvents = entities.SelectMany(e => e.DomainEvents);
        entities.ForEach(e => e.DomainEvents.Clear());
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
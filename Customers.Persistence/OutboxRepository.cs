using Customers.Persistence.AppContext;
using Customers.Persistence.Entities;
using Customers.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence;

public class OutboxRepository
{
    private readonly CustomerDbContext _context;

    public OutboxRepository(CustomerDbContext context)
    {
        _context = context;
    }

    public Task<List<Outbox>> GetNotProccedAsync(CancellationToken cancellationToken)
    {
        return _context.OutboxMessages.Where(o => o.Status == OutboxStatus.Pending).ToListAsync();
    }

    public Task CompleteMessagesAsync(List<Outbox> messages, CancellationToken cancellationToken)
    {
        messages.ForEach(m=>m.Status = OutboxStatus.Published);
        return Task.CompletedTask;
    }
}
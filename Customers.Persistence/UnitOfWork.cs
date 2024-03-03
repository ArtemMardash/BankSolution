using Customers.Application.Interfaces;
using Customers.Persistence.AppContext;

namespace Customers.Persistence;

public class UnitOfWork: IDisposable, IUnitOfWork
{
    private readonly CustomerDbContext _context;

    public UnitOfWork(CustomerDbContext context)
    {
        _context = context;
    }


    public void Dispose()
    {
        _context.Dispose();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
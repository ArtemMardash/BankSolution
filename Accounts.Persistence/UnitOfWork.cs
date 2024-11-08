using Accounts.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Persistence;

public class UnitOfWork: IDisposable
{
    private readonly AccountDbContext _context;

    public UnitOfWork(AccountDbContext context)
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

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Migrate()
    {
        _context.Database.Migrate();
    }
}
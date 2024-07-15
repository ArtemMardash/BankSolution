namespace Customers.Application.Interfaces;

public interface IUnitOfWork
{
  void SaveChanges();

  Task SaveChangesAsync(CancellationToken cancellationToken);
}
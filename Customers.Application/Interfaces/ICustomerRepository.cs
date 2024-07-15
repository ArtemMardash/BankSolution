using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface ICustomerRepository
{
    /// <summary>
    /// Method to get data about customer async
    /// </summary>
    Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Method to add new customer to data base
    /// </summary>
    Task AddAsync(Customer customer, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
}
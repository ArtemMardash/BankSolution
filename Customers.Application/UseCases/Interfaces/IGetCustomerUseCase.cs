using Customers.Domain.Entities;

namespace Customers.Application.UseCases.Interfaces;

public interface IGetCustomerUseCase
{
    public Task<Customer> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
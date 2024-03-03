using Customers.Application.Interfaces;
using Customers.Application.UseCases.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.UseCases;

public class GetCustomerUseCase:IGetCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public Task<Customer> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _customerRepository.GetAsync(id, cancellationToken);
    }
}
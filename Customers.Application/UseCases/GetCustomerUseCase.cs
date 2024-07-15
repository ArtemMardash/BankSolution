using Customers.Application.Dtos;
using Customers.Application.Dtos.Responses;
using Customers.Application.Interfaces;
using MediatR;

namespace Customers.Application.UseCases;

public class GetCustomerUseCase: IRequestHandler<GetCustomerRequest, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(request.Id, cancellationToken);
        return new CustomerDto
        {
            Id = customer.Id,
            FullName = customer.FullName.GetString(),
            PhoneNumber = customer.Contacts.PhoneNumber,
            Email = customer.Contacts.Email.Value,
            MailAddress = $"{customer.MailAddress.Value}, {customer.MailAddress.ZipCode}",
            BillingAddress = $"{customer.BillingAddress.Value}, {customer.BillingAddress.ZipCode}"
        };
    }
}
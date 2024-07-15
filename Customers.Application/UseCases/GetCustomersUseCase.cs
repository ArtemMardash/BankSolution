using Customers.Application.Dtos;
using Customers.Application.Dtos.Responses;
using Customers.Application.Interfaces;
using MediatR;

namespace Customers.Application.UseCases;

public class GetCustomersUseCase: IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    private readonly ICustomerRepository _repository;

    public GetCustomersUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);
        return new GetCustomersResponse
        {
            Customers = customers.Select(c =>
            {
                return new CustomerDto
                {
                    Id = c.Id,
                    FullName = c.FullName.GetString(),
                    PhoneNumber = c.Contacts.PhoneNumber,
                    Email = c.Contacts.Email.Value,
                    MailAddress = $"{c.MailAddress.Value}, {c.MailAddress.ZipCode}",
                    BillingAddress = $"{c.BillingAddress.Value}, {c.BillingAddress.ZipCode}"
                };
            }).ToList()
        };
    }
}
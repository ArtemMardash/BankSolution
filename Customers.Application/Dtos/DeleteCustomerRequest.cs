using MediatR;

namespace Customers.Application.Dtos;

public class DeleteCustomerRequest: IRequest
{
    public Guid Id { get; set; }
}
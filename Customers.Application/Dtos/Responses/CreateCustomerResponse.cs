using MediatR;

namespace Customers.Application.Dtos.Responses;

public class CreateCustomerResponse : IRequest
{
    public Guid Id { get; set; }
}
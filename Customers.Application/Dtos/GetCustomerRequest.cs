
using Customers.Application.Dtos.Responses;
using MediatR;

namespace Customers.Application.Dtos;

public class GetCustomerRequest: IRequest<GetCustomerResponse>
{
    public Guid Id { get; set; }
}
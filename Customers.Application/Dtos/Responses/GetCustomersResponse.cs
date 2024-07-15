using Customers.Domain.Entities;

namespace Customers.Application.Dtos.Responses;

public class GetCustomersResponse
{
    public List<CustomerDto> Customers { get; set; } 
}
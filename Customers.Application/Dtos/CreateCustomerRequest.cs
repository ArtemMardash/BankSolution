using Customers.Application.Dtos.Responses;
using MediatR;

namespace Customers.Application.Dtos;

public class CreateCustomerRequest: IRequest<CreateCustomerResponse>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public ContactsDto Contacts { get; set; }
    
    public AddressDto MailAddress { get; set; }
    
    public AddressDto BillingAddress { get; set; }
}

public class ContactsDto
{
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
}

public class AddressDto
{
    public string Address { get; set; }
    
    public string ZipCode { get; set; }
}

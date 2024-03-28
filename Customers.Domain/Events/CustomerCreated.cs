using MediatR;

namespace Customers.Domain.Events;

public class CustomerCreated : INotification
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Guid Id { get; set; }
}
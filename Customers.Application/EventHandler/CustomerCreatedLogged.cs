using Customers.Domain.Events;
using MediatR;

namespace Customers.Application.EventHandler;

public class CustomerCreatedLogged: INotificationHandler<CustomerCreated>
{
    public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{notification.FirstName} {notification.LastName} a new customer with Id {notification.Id}");
        return Task.CompletedTask;
    }
}
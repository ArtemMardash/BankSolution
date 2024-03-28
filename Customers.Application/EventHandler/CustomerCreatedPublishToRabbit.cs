using Customers.Application.Interfaces;
using Customers.Domain.Events;
using MediatR;

namespace Customers.Application.EventHandler;

public class CustomerCreatedPublishToRabbit: INotificationHandler<CustomerCreated>
{
    private readonly IBrokerPublisher _publisher;

    public CustomerCreatedPublishToRabbit(IBrokerPublisher publisher)
    {
        _publisher = publisher;
    }
    public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
    {
       return _publisher.PublishCustomerCreatedAsync(notification, cancellationToken);
    }
}
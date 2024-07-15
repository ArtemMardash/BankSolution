using Customers.Application.Interfaces;
using Customers.Domain.Events;
using MediatR;

namespace Customers.Application.EventHandler;

public class CustomerDeletedPublishToRabbit: INotificationHandler<CustomerDeleted>
{
    private readonly IBrokerPublisher _publisher;

    public CustomerDeletedPublishToRabbit(IBrokerPublisher publisher)
    {
        _publisher = publisher;
    }
    public Task Handle(CustomerDeleted notification, CancellationToken cancellationToken)
    {
        return _publisher.PublishCustomerDeletedAsync(notification, cancellationToken);
    }
}
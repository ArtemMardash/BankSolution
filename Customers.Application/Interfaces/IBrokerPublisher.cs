using Customers.Domain.Events;

namespace Customers.Application.Interfaces;

public interface IBrokerPublisher
{
    /// <summary>
    /// Method to publish a message async
    /// </summary>
    Task PublishCustomerCreatedAsync(CustomerCreated customerCreated, CancellationToken cancellationToken);

    Task PublishCustomerDeletedAsync(CustomerDeleted customerDeleted, CancellationToken cancellationToken);
}
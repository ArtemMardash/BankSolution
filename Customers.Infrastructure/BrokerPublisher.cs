using Customers.Application.Interfaces;
using Customers.Domain.Events;
using MassTransit;
using SharedKernal;

namespace Customers.Infrastructure;

/// <summary>
/// Realization of all methods of IBrokerPublisher
/// </summary>
public class BrokerPublisher : IBrokerPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Realization of all methods of IBrokPublisher
    /// </summary>
    /// <param name="publishEndpoint"></param>
    public BrokerPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Publish message of created sutomer
    /// </summary>
    public Task PublishCustomerCreatedAsync(CustomerCreated customerCreated, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish<ICustomerCreated>(new
        {
            customerCreated.FirstName,
            customerCreated.LastName,
            customerCreated.Id
        }, cancellationToken);
    }

    /// <summary>
    /// Publish message of deleted customer
    /// </summary>
    public Task PublishCustomerDeletedAsync(CustomerDeleted customerDeleted, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish<ICustomerDeleted>(new
        {
            customerDeleted.Id
        }, cancellationToken);
    }
}
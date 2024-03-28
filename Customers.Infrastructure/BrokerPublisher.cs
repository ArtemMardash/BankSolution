using System.Text;
using System.Text.Unicode;
using Customers.Application.Interfaces;
using Customers.Domain.Events;
using Customers.Domain.ValueObjects;
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

    public Task PublishCustomerCreatedAsync(CustomerCreated customerCreated, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish<ICustomerCreated>(new
        {
            customerCreated.FirstName,
            customerCreated.LastName,   
            customerCreated.Id
        }, cancellationToken);
    }
}   
using System.Text;
using System.Text.Unicode;
using Customers.Application.Interfaces;
using MassTransit;

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
    ///Method to publish message async
    /// </summary>
    public async Task PublishAsync<T>(T message, string exchange, string? routingKey,
        CancellationToken cancellationToken)
    {
        var newMessage = Encoding.UTF8.GetBytes(message.ToString());
        await _publishEndpoint.Publish(newMessage);
    }

    /// <summary>
    /// Method to publish message
    /// </summary>
    public void Publish<T>(T message, string exchange, string? routingKey)
    {
        var newMessage = Encoding.UTF8.GetBytes(message.ToString());
        _publishEndpoint.Publish(newMessage);
    }
}
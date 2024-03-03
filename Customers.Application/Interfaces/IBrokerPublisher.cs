namespace Customers.Application.Interfaces;

public interface IBrokerPublisher
{
    /// <summary>
    /// Method to publish a message async
    /// </summary>
    Task PublishAsync<T>(T message, string exchange, string? routingKey, CancellationToken cancellationToken);

    /// <summary>
    /// Method to publish message
    /// </summary>
    void Publish<T>(T message, string exchange, string? routingKey);
}
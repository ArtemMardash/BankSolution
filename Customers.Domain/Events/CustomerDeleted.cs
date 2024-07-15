using MediatR;

namespace Customers.Domain.Events;

/// <summary>
/// Customer deleted notification
/// </summary>
public class CustomerDeleted: INotification
{
    /// <summary>
    /// Id of customer
    /// </summary>
    public Guid Id { get; set; }
}
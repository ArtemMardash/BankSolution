using MediatR;

namespace Customers.Domain.Events;

/// <summary>
/// Customer created notification
/// </summary>
public class CustomerCreated : INotification
{
    /// <summary>
    /// First name of customer
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last name of customer
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Id of customer 
    /// </summary>
    public Guid Id { get; set; }
}
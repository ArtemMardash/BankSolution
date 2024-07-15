using MediatR;

namespace Accounts.Domain.Events;

/// <summary>
/// Notification about changed balance 
/// </summary>
public class AccountBalanceChanged: INotification
{
    /// <summary>
    /// Id of customer
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// public id of account
    /// </summary>
    public string PublicId { get; set; }
    
    /// <summary>
    /// Old balance
    /// </summary>
    public decimal OldBalance { get; set; }
    
    /// <summary>
    /// New balance after deposit/withdraw
    /// </summary>
    public decimal NewBalance { get; set; }
}
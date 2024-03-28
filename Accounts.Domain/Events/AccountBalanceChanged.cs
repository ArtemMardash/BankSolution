using MediatR;

namespace Accounts.Domain.Events;

public class AccountBalanceChanged: INotification
{
    public Guid CustomerId { get; set; }
    
    public string PublicId { get; set; }
    
    public decimal OldBalance { get; set; }
    
    public decimal NewBalance { get; set; }
}
using Accounts.Domain.Events;
using MediatR;

namespace Accounts.Application.EventHandlers;

public class BalanceChangedLogger: INotificationHandler<AccountBalanceChanged>
{
    public Task Handle(AccountBalanceChanged notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"The balance was changed for an account {notification.PublicId} from {notification.OldBalance} to {notification.NewBalance}");
        return Task.CompletedTask;
    }
}
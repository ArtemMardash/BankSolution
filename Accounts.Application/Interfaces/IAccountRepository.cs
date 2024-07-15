using Accounts.Domain.Entities;

namespace Accounts.Application.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetCustomerAccountsAsync(Guid customerId, CancellationToken cancellationToken);

    Task<string> CreateNewAccountAsync(Account account, CancellationToken cancellationToken);

    Task<Account?> GetAccountAsync(string publicId, CancellationToken cancellationToken);

    Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);

    Task DeleteAsync(List<Account> accounts, CancellationToken cancellationToken);

}
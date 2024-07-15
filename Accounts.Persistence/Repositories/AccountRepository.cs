using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using Accounts.Domain.Events;
using Accounts.Domain.ValueObjects;
using Accounts.Persistence.AppContext;
using Accounts.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Persistence.Repositories;

/// <summary>
/// Repository of account
/// </summary>
public class AccountRepository : IAccountRepository
{
    private readonly UnitOfWork _unitOfWork;
    private readonly AccountDbContext _context;

    /// <summary>
    /// Repository of account
    /// </summary>
    public AccountRepository(UnitOfWork unitOfWork, AccountDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    /// <summary>
    /// Method to get customer accounts
    /// </summary>
    public async Task<List<Account>> GetCustomerAccountsAsync(Guid customerId, CancellationToken cancellationToken)
    {

        var list = await _context.Account.Where(a => a.CustomerId == customerId)
            .Select(a=>FromAccountDbToAccount(a))
            .ToListAsync(cancellationToken);
        return list;
    }

    /// <summary>
    /// Method to get account
    /// </summary>
    /// <returns></returns>
    public async Task<Account?> GetAccountAsync(string publicId, CancellationToken cancellationToken)
    {
        var accountDb = await _context.Account.FirstOrDefaultAsync(a => a.PublicId == publicId, cancellationToken);
        var account = FromAccountDbToAccount(accountDb);
        return account;
    }

    /// <summary>
    /// Method to create new account
    /// </summary>
    public async Task<string> CreateNewAccountAsync(Account account, CancellationToken cancellationToken)
    {
        var accountDb = FromAccountToAccountDb(account);
        _context.Account.Add(accountDb);
        await _unitOfWork.SaveChangesAsync();
        return accountDb.PublicId;
    }

    /// <summary>
    /// Method to update data of account
    /// </summary>
    public async Task UpdateAccountAsync(Account account, CancellationToken cancellationToken)
    {
        var accountDb = await _context.Account.FirstOrDefaultAsync(a => a.PublicId == account.Id.PublicId);
        if (accountDb == null)
        {
            //кидаем исключения
        }

        if (accountDb.Balance != account.Balance)
        {
            var balanceChanged = new AccountBalanceChanged
            {
                CustomerId = account.CustomerId,
                NewBalance = account.Balance,
                OldBalance = accountDb.Balance,
                PublicId = account.Id.PublicId
            };
            accountDb.DomainEvents.Add(balanceChanged);
            accountDb.Balance = account.Balance;
        }

        accountDb.Status = (int)account.Status;
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Method to delete account
    /// </summary>
    public async Task DeleteAsync(List<Account> accounts, CancellationToken cancellationToken)
    {
        //var accountsDb = accounts.Select(a => FromAccountToAccountDb(a));
        var accountsDb = _context.ChangeTracker
            .Entries<AccountDb>()
            .Where(aDb => accounts.Any(a => a.CustomerId == aDb.Entity.CustomerId))
            .Select(aDb => aDb.Entity)
            .ToList();
        _context.Account.RemoveRange(accountsDb);
        await _unitOfWork.SaveChangesAsync();
    } 
    private static Account? FromAccountDbToAccount(AccountDb? accountDb)
    {
        return accountDb == null
            ? null
            : new Account(new AccountId(accountDb.Id, accountDb.PublicId), accountDb.CustomerId,
                accountDb.Balance, (AccountStatus) accountDb.Status);
    }

    /// <summary>
    /// Method to convert from entity to db entity
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    private static AccountDb FromAccountToAccountDb(Account account)
    {
        return new AccountDb
        {
            Id = account.Id.SystemId,
            PublicId = account.Id.PublicId,
            CustomerId = account.CustomerId,
            Balance = account.Balance,
            Status = (int)account.Status
        };
    }
    
    
}
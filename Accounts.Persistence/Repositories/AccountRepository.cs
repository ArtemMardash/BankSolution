using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using Accounts.Domain.Events;
using Accounts.Domain.ValueObjects;
using Accounts.Persistence.AppContext;
using Accounts.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UnitOfWork _unitOfWork;
    private readonly AccountDbContext _context;

    public AccountRepository(UnitOfWork unitOfWork, AccountDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public Task<List<Account>> GetCustomerAccountsAsync(Guid customerId, CancellationToken cancellationToken)
    {

        return _context.Account.Where(a => a.CustomerId == customerId)
            .Select(a=>FromAccountDbToAccount(a))
            .ToListAsync(cancellationToken);
    }

    public async Task<Account?> GetAccountAsync(string publicId, CancellationToken cancellationToken)
    {
        var accountDb = await _context.Account.FirstOrDefaultAsync(a => a.PublicId == publicId, cancellationToken);
        var account = FromAccountDbToAccount(accountDb);
        return account;
    }

    public async Task<string> CreateNewAccountAsync(Account account, CancellationToken cancellationToken)
    {
        var accountDb = FromAccountToAccountDb(account);
        _context.Account.Add(accountDb);
        await _unitOfWork.SaveChangesAsync();
        return accountDb.PublicId;
    }

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
    
    private static Account? FromAccountDbToAccount(AccountDb? accountDb)
    {
        return accountDb == null
            ? null
            : new Account(new AccountId(accountDb.Id, accountDb.PublicId), accountDb.CustomerId,
                accountDb.Balance, (AccountStatus) accountDb.Status);
    }

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
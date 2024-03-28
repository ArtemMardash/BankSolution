using Accounts.Domain.Enums;
using Accounts.Domain.Exceptions;
using Accounts.Domain.ValueObjects;

namespace Accounts.Domain.Entities;

public class Account
{
    public AccountId Id { get; }

    public Guid CustomerId { get; }

    /// <summary>
    /// Balance in the account
    /// </summary>
    public decimal Balance { get; private set; }
    
    public AccountStatus Status { get; private set; }


    public Account()
    {
    }

    public Account(AccountId id, Guid customerId, decimal balance, AccountStatus status)
    {
        Id = id;
        if (customerId == Guid.Empty)
        {
            throw new InvalidIdException("id is invalid");
        }

        CustomerId = customerId;
        SetBalance(balance);
        Status = status;
    }

    public Account(Guid customerId, decimal balance, AccountStatus status = AccountStatus.Active)
    {
        var id = Guid.NewGuid();
        var publicId = Convert.ToBase64String(id.ToByteArray());
        Id = new AccountId(id, publicId.Substring(0, 8));
        CustomerId = customerId;
        SetBalance(balance);
        Status = status;
    }

    private void SetBalance(decimal input)
    {
        if (input < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Balance = input;
    }


    /// <summary>
    /// Check amount >0 
    /// </summary>
    public void Withdraw(decimal amount)
    {
        if (amount <0 || amount > Balance)
        {
            throw new InvalidAmountException("amount should be bigger then 0 and smaller than balance");
        }
        if (Status is AccountStatus.Blocked or AccountStatus.Closed or AccountStatus.Unknown)
        {
            throw new InvalidStatusToChangeBalance("You cannot withdraw money because of the invalid status");
        }

        Balance -= amount;
    }

    /// <summary>
    /// Check amount > 0
    /// </summary>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException("amount can not be zero or smaller");
        }
        if (Status is AccountStatus.Blocked or AccountStatus.Closed or AccountStatus.Unknown)
        {
            throw new InvalidStatusToChangeBalance("You cannot make a deposit because of the invalid status");
        }

        Balance += amount;
    }

    public void SetStatus(AccountStatus newStatus)
    {
        switch (newStatus)
        {
            case AccountStatus.Active when Status is AccountStatus.Blocked:
                Status = newStatus;
                break;
            case AccountStatus.Blocked when Status is AccountStatus.Active:
                Status = newStatus;
                break;
            case AccountStatus.Closed when Balance is 0:
                Status = newStatus;
                break;
            default:
                throw new InvalidAccountStatus("Invalid status of account");
        }
    }
}
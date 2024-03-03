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


    public Account()
    {
    }

    public Account(AccountId id, Guid customerId, decimal balance)
    {
        Id = id;
        if (customerId == Guid.Empty)
        {
            throw new InvalidIdException("id is invalid");
        }

        CustomerId = customerId;
        SetBalance(balance);
    }

    public Account(Guid customerId, decimal balance)
    {
        var id = Guid.NewGuid();
        var publicId = Convert.ToBase64String(id.ToByteArray());
        Id = new AccountId(id, publicId.Substring(0, 8));
        CustomerId = customerId;
        SetBalance(balance);
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

        Balance += amount;
    }
}
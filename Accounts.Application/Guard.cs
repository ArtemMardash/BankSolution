using Accounts.Application.Exceptions;
using Accounts.Domain.Entities;

namespace Accounts.Application;

public static class Guard
{
    public static void ThrowIfAccountNull(Account? account)
    {
        if (account == null)
        {
            throw new AccountNotFoundException("There is no account with that public id");
        }
    }
}
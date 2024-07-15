namespace Accounts.Application.Dtos.Responses;

/// <summary>
/// Dto to return list of accounts
/// </summary>
public class AccountsListDto
{
    public Guid CustomerId { get; set; }
    
    public List<AccountDto> Accounts { get; set; }
}

/// <summary>
/// dto to return account
/// </summary>
public class AccountDto
{
    public string PublicId { get; set; }
}
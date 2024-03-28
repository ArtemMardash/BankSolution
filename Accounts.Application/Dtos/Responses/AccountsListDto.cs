namespace Accounts.Application.Dtos.Responses;

public class AccountsListDto
{
    public Guid CustomerId { get; set; }
    
    public List<AccountDto> Accounts { get; set; }
}

public class AccountDto
{
    public string PublicId { get; set; }
}
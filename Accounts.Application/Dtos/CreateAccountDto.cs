using Accounts.Application.Dtos.Responses;
using MediatR;

namespace Accounts.Application.Dtos;

public class CreateAccountDto: IRequest<AccountDto>
{
    public decimal Balance { get; set; }
    
    public Guid CustomerId { get; set; }
}
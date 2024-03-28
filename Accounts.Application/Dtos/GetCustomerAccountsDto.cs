using Accounts.Application.Dtos.Responses;
using MediatR;

namespace Accounts.Application.Dtos;

public class GetCustomerAccountsDto: IRequest<AccountsListDto>
{
    public Guid CustomerId { get; set; }
}
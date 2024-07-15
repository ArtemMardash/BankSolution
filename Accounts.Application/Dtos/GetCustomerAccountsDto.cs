using Accounts.Application.Dtos.Responses;
using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// dto to get accounts of customer
/// </summary>
public class GetCustomerAccountsDto: IRequest<AccountsListDto>
{
    public Guid CustomerId { get; set; }
}
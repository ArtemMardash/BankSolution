using Accounts.Application.Dtos.Responses;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using MediatR;

namespace Accounts.Application.Dtos;

public class GetCustomerAccountsDto: IRequest<AccountsListDto>
{
    public Guid CustomerId { get; set; }
}
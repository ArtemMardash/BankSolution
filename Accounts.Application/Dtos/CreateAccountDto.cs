using Accounts.Application.Dtos.Responses;
using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// Dto to create accoumt
/// </summary>
public class CreateAccountDto: IRequest<AccountDto>
{
    /// <summary>
    /// Balance Of account
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// id of customer fwho created accoumt
    /// </summary>
    public Guid CustomerId { get; set; }
}
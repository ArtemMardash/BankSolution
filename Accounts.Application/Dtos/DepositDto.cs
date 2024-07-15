using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// Dto to make a deposit
/// </summary>
public class DepositDto: IRequest
{
    public string PublicId { get; set; }
    
    public decimal BalanceToDeposit { get; set; }
}
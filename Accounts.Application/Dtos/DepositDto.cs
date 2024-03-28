using MediatR;

namespace Accounts.Application.Dtos;

public class DepositDto: IRequest
{
    public string PublicId { get; set; }
    
    public decimal BalanceToDeposit { get; set; }
}
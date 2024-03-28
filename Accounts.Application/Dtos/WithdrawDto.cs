using MediatR;

namespace Accounts.Application.Dtos;

public class WithdrawDto: IRequest
{
    public string PublicId { get; set; }
    
    public decimal AmountToWithdraw { get; set; }
}
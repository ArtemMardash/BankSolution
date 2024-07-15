using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// Dto to withdraw 
/// </summary>
public class WithdrawDto: IRequest
{
    /// <summary>
    /// public id of account
    /// </summary>
    public string PublicId { get; set; }
    
    /// <summary>
    /// amount to withdraw from account
    /// </summary>
    public decimal AmountToWithdraw { get; set; }
}
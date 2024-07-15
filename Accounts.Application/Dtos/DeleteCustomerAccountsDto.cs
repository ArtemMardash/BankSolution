using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// Dto to delete account
/// </summary>
public class DeleteCustomerAccountsDto: IRequest
{
    /// <summary>
    /// Id of customer
    /// </summary>
    public Guid CustomerId { get; set; }
}
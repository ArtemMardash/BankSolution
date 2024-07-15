using Accounts.Domain.Enums;
using MediatR;

namespace Accounts.Application.Dtos;

/// <summary>
/// Dto to change status
/// </summary>
public class ChangeStatusDto: IRequest
{
    /// <summary>
    /// public id of account
    /// </summary>
    public string PublicId { get; set; }

    /// <summary>
    /// Status of account
    /// </summary>
    public AccountStatus StatusToChange { get; set; }
}
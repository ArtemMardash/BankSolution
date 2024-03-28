using Accounts.Domain.Enums;
using MediatR;

namespace Accounts.Application.Dtos;

public class ChangeStatusDto: IRequest
{
    public string PublicId { get; set; }

    public AccountStatus StatusToChange { get; set; }
}
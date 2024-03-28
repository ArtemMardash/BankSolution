using Accounts.Application.Dtos;
using Accounts.Application.Exceptions;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using MediatR;

namespace Accounts.Application.UseCases;

public class DepositUseCase : IRequestHandler<DepositDto>
{
    private readonly IAccountRepository _repository;

    public DepositUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DepositDto request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountAsync(request.PublicId, cancellationToken);
        Guard.ThrowIfAccountNull(account);
        account.Deposit(request.BalanceToDeposit);
        await _repository.UpdateAccountAsync(account, cancellationToken);
    }
}
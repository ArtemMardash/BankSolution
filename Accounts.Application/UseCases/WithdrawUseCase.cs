using Accounts.Application.Dtos;
using Accounts.Application.Exceptions;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

public class WithdrawUseCase : IRequestHandler<WithdrawDto>
{
    private readonly IAccountRepository _repository;

    public WithdrawUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }


    public async Task Handle(WithdrawDto request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountAsync(request.PublicId, cancellationToken);
        Guard.ThrowIfAccountNull(account);
        account.Withdraw(request.AmountToWithdraw);
        await _repository.UpdateAccountAsync(account, cancellationToken);
    }
}
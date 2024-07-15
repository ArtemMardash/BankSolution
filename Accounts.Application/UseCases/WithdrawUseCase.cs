using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

/// <summary>
/// Use case to withdraw from account
/// </summary>
public class WithdrawUseCase : IRequestHandler<WithdrawDto>
{
    private readonly IAccountRepository _repository;

    /// <summary>
    /// Use case to withdraw from account
    /// </summary>
    /// <param name="repository"></param>
    public WithdrawUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Method to withdraw from account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(WithdrawDto request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountAsync(request.PublicId, cancellationToken);
        Guard.ThrowIfAccountNull(account);
        account.Withdraw(request.AmountToWithdraw);
        await _repository.UpdateAccountAsync(account, cancellationToken);
    }
}
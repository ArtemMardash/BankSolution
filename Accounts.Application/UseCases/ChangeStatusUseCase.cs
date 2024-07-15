using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

/// <summary>
/// Use case to change status of account
/// </summary>
public class ChangeStatusUseCase : IRequestHandler<ChangeStatusDto>
{
    private readonly IAccountRepository _repository;

    /// <summary>
    /// Use case to change status of account
    /// </summary>
    /// <param name="repository"></param>
    public ChangeStatusUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Method to change status of account
    /// </summary>
    public async Task Handle(ChangeStatusDto request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountAsync(request.PublicId, cancellationToken);
        Guard.ThrowIfAccountNull(account);
        account.SetStatus(request.StatusToChange);
        await _repository.UpdateAccountAsync(account, cancellationToken);
    }
}
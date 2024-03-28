using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

public class ChangeStatusUseCase : IRequestHandler<ChangeStatusDto>
{
    private readonly IAccountRepository _repository;

    public ChangeStatusUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeStatusDto request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountAsync(request.PublicId, cancellationToken);
        Guard.ThrowIfAccountNull(account);
        account.SetStatus(request.StatusToChange);
        await _repository.UpdateAccountAsync(account, cancellationToken);
    }
}
using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

/// <summary>
/// Use case to delete an account 
/// </summary>
public class DeleteAccountUseCase: IRequestHandler<DeleteCustomerAccountsDto>
{
    private readonly IAccountRepository _repository;

    /// <summary>
    /// Use case to delete an account
    /// </summary>
    /// <param name="repository"></param>
    public DeleteAccountUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// Method to delete account
    /// </summary>
    public async Task Handle(DeleteCustomerAccountsDto request, CancellationToken cancellationToken)
    {
        var accounts = await _repository.GetCustomerAccountsAsync(request.CustomerId, cancellationToken);
        
        await _repository.DeleteAsync(accounts.Where(a=>a.CanDelete).ToList(), cancellationToken);
    }
}
using Accounts.Application.Dtos;
using Accounts.Application.Dtos.Responses;
using Accounts.Application.Interfaces;
using MediatR;

namespace Accounts.Application.UseCases;

/// <summary>
/// Use case to get customer accounts
/// </summary>
public class GetCustomerAccountsUseCase: IRequestHandler<GetCustomerAccountsDto, AccountsListDto>
{
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// Use case to get customer accounts
    /// </summary>
    /// <param name="accountRepository"></param>
    public GetCustomerAccountsUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    /// <summary>
    /// Method to get customer accounts
    /// </summary>
    public async Task<AccountsListDto> Handle(GetCustomerAccountsDto request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetCustomerAccountsAsync(request.CustomerId, cancellationToken);
        return new AccountsListDto
        {
            CustomerId = request.CustomerId,
            Accounts = accounts.Select(a => new AccountDto
            {
                PublicId = a.Id.PublicId
            }).ToList()
        };
    }
}
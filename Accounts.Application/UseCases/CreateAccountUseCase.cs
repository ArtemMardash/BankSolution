using Accounts.Application.Dtos;
using Accounts.Application.Dtos.Responses;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using MediatR;

namespace Accounts.Application.UseCases;

/// <summary>
/// Use case to create a new account
/// </summary>
public class CreateAccountUseCase : IRequestHandler<CreateAccountDto, AccountDto>
{
    private readonly IAccountRepository _repository;

    /// <summary>
    /// Use case to create a new account
    /// </summary>
    /// <param name="repository"></param>
    public CreateAccountUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Method to create a new account
    /// </summary>
    public async Task<AccountDto> Handle(CreateAccountDto request, CancellationToken cancellationToken)
    {
        var publicId=await  _repository.CreateNewAccountAsync(new Account(request.CustomerId, request.Balance), cancellationToken);

        return new AccountDto
        {
            PublicId = publicId
        };  
    }
}
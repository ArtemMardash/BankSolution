using Accounts.Application.Dtos;
using Accounts.Application.Dtos.Responses;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using MediatR;

namespace Accounts.Application.UseCases;

public class CreateAccountUseCase : IRequestHandler<CreateAccountDto, AccountDto>
{
    private readonly IAccountRepository _repository;

    public CreateAccountUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<AccountDto> Handle(CreateAccountDto request, CancellationToken cancellationToken)
    {
        var publicId=await  _repository.CreateNewAccountAsync(new Account(request.CustomerId, request.Balance), cancellationToken);

        return new AccountDto
        {
            PublicId = publicId
        };
    }
}
using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using Customers.Application.Interfaces;
using Customers.Domain.Events;
using MassTransit;
using SharedKernal;

namespace Accounts.Infrastructure;

public class CustomerCreatedConsumer : IConsumer<ICustomerCreated>
{
    private readonly IAccountRepository _accountRepository;

    public CustomerCreatedConsumer(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task Consume(ConsumeContext<ICustomerCreated> context)
    {
        var dto = new CreateAccountDto
        {
            Balance = 0,
            CustomerId = context.Message.Id
        };
        await _accountRepository.CreateNewAccountAsync(new Account(dto.CustomerId, dto.Balance, AccountStatus.Active), CancellationToken.None);
    }
}
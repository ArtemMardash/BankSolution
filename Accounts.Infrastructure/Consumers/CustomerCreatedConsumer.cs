using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using MassTransit;
using SharedKernal;

namespace Accounts.Infrastructure.Consumers;

/// <summary>
/// Customer created consumer
/// </summary>
public class CustomerCreatedConsumer : IConsumer<ICustomerCreated>
{
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// Customer created consumer
    /// </summary>
    public CustomerCreatedConsumer(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    /// <summary>
    /// Methdo to return message and create account when created customer
    /// </summary>
    public async Task Consume(ConsumeContext<ICustomerCreated> context)
    {
        var dto = new CreateAccountDto
        {
            Balance = 0,
            CustomerId = context.Message.Id
        };
        var publicId = await _accountRepository.CreateNewAccountAsync(new Account(dto.CustomerId, dto.Balance, AccountStatus.Active), CancellationToken.None);
        Console.WriteLine($"Account {publicId} was created for Customer with id {dto.CustomerId} ");
        
    }
}
using Accounts.Application.Dtos;
using Accounts.Application.Interfaces;
using MassTransit;
using MediatR;
using SharedKernal;

namespace Accounts.Infrastructure.Consumers;

/// <summary>
/// Customer Deleted consumer
/// </summary>
public class CustomerDeletedConsumer: IConsumer<ICustomerDeleted>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Customer Deleted consumer
    /// </summary>
    public CustomerDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Method to return message and delete account when customer deleted
    /// </summary>
    /// <param name="context"></param>
    public async Task Consume(ConsumeContext<ICustomerDeleted> context)
    {
         await _mediator.Send(new DeleteCustomerAccountsDto { CustomerId = context.Message.Id });
         Console.WriteLine($"Customer with id {context.Message.Id} was deleted and his accounts too");
    }
}
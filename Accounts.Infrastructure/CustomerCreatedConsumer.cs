using System.Threading.Channels;
using MassTransit;
using SharedKernal;

namespace Accounts.Infrastructure;

public class CustomerCreatedConsumer : IConsumer<ICustomerCreated>
{
    public Task Consume(ConsumeContext<ICustomerCreated> context)
    {
        Console.WriteLine("Message about created customer");
        return Task.CompletedTask;
    }
}
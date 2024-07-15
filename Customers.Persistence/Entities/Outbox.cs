using Customers.Persistence.Enums;

namespace Customers.Persistence.Entities;

public class Outbox
{
    public Guid Id { get; set; }

    public OutboxStatus Status { get; set; }

    public string PayLoad { get; set; }
    
    public string MessageType{ get; set; }
    
}
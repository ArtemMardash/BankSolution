using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Customers.Persistence.Entities;

public class Entity
{
    [NotMapped]
    public List<INotification> DomainEvents { get;} = new List<INotification>();
}
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Customers.Domain.Entities;

public class BaseEntity
{
    [NotMapped] 
    public List<INotification> DomainEvents { get;} = new List<INotification>();
}
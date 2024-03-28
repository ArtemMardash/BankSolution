using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Accounts.Persistence.Entities;

public class BaseEntity
{
    [NotMapped]
    public List<INotification> DomainEvents { get;} = new List<INotification>();
}
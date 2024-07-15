using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Accounts.Persistence.Entities;

/// <summary>
/// Entity which contain notification
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// List of notification  
    /// </summary>
    [NotMapped]
    public List<INotification> DomainEvents { get;} = new List<INotification>();
}
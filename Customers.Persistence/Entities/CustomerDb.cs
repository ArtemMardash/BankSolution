namespace Customers.Persistence.Entities;

public class CustomerDb : Entity
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Full name
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Contacts
    /// </summary>
    public ContactsDb Contacts { get; set; }

    /// <summary>
    /// Mail address
    /// </summary>
    public string MailAddress { get; set; }
    
    /// <summary>
    /// Billing address
    /// </summary>
    public string BillingAddress { get; set; }
}
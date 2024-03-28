using Customers.Domain.Events;
using Customers.Domain.Exceptions;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities;

/// <summary>
/// Class of the customers
/// </summary>
public class Customer: BaseEntity
{
    /// <summary>
    /// Id of the customer
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Full name of the customer
    /// </summary>
    public FullName FullName { get; private set; }

    /// <summary>
    /// Phone number and email of the customer
    /// </summary>
    public Contacts Contacts { get; private set; }

    /// <summary>
    /// mail address of the customer
    /// </summary>
    public Address MailAddress { get; private set; }

    /// <summary>
    /// billing address of the customer
    /// </summary>
    public Address BillingAddress { get; private set; }

    /// <summary>
    /// Customers
    /// </summary>
    public Customer(Guid id, FullName fullName, Contacts contacts, Address mailAddress, Address billingAddress)
    {
        if (id == Guid.Empty)
        {
            DomainExceptions.ThrowException<InvalidIdException>("Id can not be empty", args: nameof(id));
        }

        Id = id;
        FullName = fullName;
        Contacts = contacts;
        MailAddress = mailAddress;
        BillingAddress = billingAddress;
        DomainEvents.Add(new CustomerCreated{Id = id, FirstName = FullName.FirstName, LastName = FullName.LastName});
    }

    /// <summary>
    ///  Customers
    /// </summary>
    public Customer(FullName fullName, Contacts contacts, Address mailAddress, Address billingAddress)
        : this(Guid.NewGuid(), fullName, contacts, mailAddress, billingAddress)

    {
    }


    /// <summary>
    /// Method to change full name
    /// </summary>
    public void ChangeFullName(string newFirstName, string newLastName)
    {
        var newFullName = new FullName(new FirstName(newFirstName), new LastName(newLastName));
        FullName = newFullName;
    }

    /// <summary>
    /// Method to change billing address
    /// </summary>
    public void ChangeBillingAddress(string newZipCOde, string newAddress)
    {
        var newBillingAddress = new Address(newAddress, newZipCOde);
        BillingAddress = newBillingAddress;
    }

    /// <summary>
    /// Method to change mail address
    /// </summary>
    public void ChangeMailAddress(string newZipCOde, string newAddress)
    {
        var newMailAddress = new Address(newAddress, newZipCOde);
        MailAddress = newMailAddress;
    }

    /// <summary>
    /// Method to change email and phone number
    /// </summary>
    public void ChangeContacts(string email, string phoneNumber)
    {
        var newContacts = new Contacts(new Email(email), phoneNumber);
        Contacts = newContacts;
    }
}
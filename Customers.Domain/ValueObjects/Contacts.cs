using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// Contacts of customer
/// </summary>
public class Contacts
{
    private const int MAX_LENGTH = 12;
    
    /// <summary>
    /// Phone number
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// email
    /// </summary>
    public Email Value { get; private set; }

    /// <summary>
    /// Contacts of customer
    /// </summary>
    public Contacts(Email value, string phoneNumber)
    {
        Value = value;
        SetPhone(phoneNumber);
    }

    /// <summary>
    /// Method to set a phone number
    /// </summary>
    public void SetPhone(string phoneNumber)
    {
        if (phoneNumber.Length == MAX_LENGTH)
        {
            PhoneNumber = phoneNumber;
        }
        else
        {
            DomainExceptions.ThrowException<InvalidContactsException>("phone number length should be 12",
                args: nameof(phoneNumber));
        }
    }
}
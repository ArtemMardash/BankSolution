using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// Email for contacts
/// </summary>
public class Email
{
    /// <summary>
    /// Email
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Email for contacts
    /// </summary>
    public Email(string value)
    {
        SetEmail(value);
    }

    /// <summary>
    /// Method to set email
    /// </summary>
    public void SetEmail(string newEmail)
    {
        if (newEmail.Contains("@") && newEmail.Contains("."))
        {
            Value = newEmail;
        }
        else
        {
            DomainExceptions.ThrowException<InvalidEmailException>("email should contain @ and .");
        }
    }
}
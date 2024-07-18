using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// First name of the customer
/// </summary>
public class FirstName
{
    private const int MAX_LENGTH = 30;
    
    /// <summary>
    /// First name
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// First name of the customer
    /// </summary>
    public FirstName(string value)
    {
        SetValue(value);
    }

    /// <summary>
    /// First name of the customer
    /// </summary>
    protected FirstName()
    {
    }

    /// <summary>
    /// Check first name 
    /// </summary>
    private void SetValue(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length > MAX_LENGTH)
        {
            DomainExceptions.ThrowException<InvalidStringValueException>("first name is too long or empty",
                args: nameof(FirstName));
        }

        Value = input.ToLower();
    }

    public static implicit operator string(FirstName fName)
    {
        return fName.Value;
    }
}
using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// Last name of the Customer
/// </summary>
public class LastName
{
    /// <summary>
    /// Max length of last name
    /// </summary>
    private const int MAX_LENGTH = 40;

    /// <summary>
    /// Last name 
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Last name of the Customer
    /// </summary>
    public LastName(string value)
    {
        SetValue(value);
    }

    /// <summary>
    /// Constructor for EF
    /// </summary>
    protected LastName()
    {
    }

    /// <summary>
    /// Check last name
    /// </summary>
    /// <param name="input"></param>
    private void SetValue(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length > MAX_LENGTH)
        {
            DomainExceptions.ThrowException<InvalidStringValueException>("Last name out of range",
                args: nameof(LastName));
        }

        Value = input.ToLower();
    }

    public static implicit operator string(LastName lName)
    {
        return lName.Value;
    }
}
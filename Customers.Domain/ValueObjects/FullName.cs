using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// Full name of the customer
/// </summary>
public class FullName
{
    /// <summary>
    /// First name 
    /// </summary>
    public FirstName FirstName { get; private set; }
    
    /// <summary>
    /// Last name 
    /// </summary>
    public LastName LastName { get; private set; }

    /// <summary>
    ///  Full name of the customer
    /// </summary>
    public FullName(FirstName firstName, LastName lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    /// <summary>
    ///  Full name of the customer
    /// </summary>
    protected FullName()
    {
    }

    /// <summary>
    /// Convert from string to FullName
    /// </summary>
    public static FullName CreateFromString(string input)
    {
        var splitedString = input.Split(' ');
        if (splitedString.Length != 2)
        {
            DomainExceptions.ThrowException<InvalidFullNameException>("Invalid convert from string to Full Name");
        }
        var firstName = input.Split(' ')[0];
        var lastName = input.Split(' ')[1];
        FullName result = null;
        try
        {
            result = new FullName(new FirstName(firstName), new LastName(lastName));
        }
        catch (InvalidStringValueException exception)
        {
            DomainExceptions.ThrowException<InvalidFullNameException>("Invalid convert from string to Full Name",
                exception);
        }

        return result;
    }

    /// <summary>
    /// Convert to string
    /// </summary>
    public string GetString()
    {
        return $"{CapitalizeFirstLetter(FirstName)} {CapitalizeFirstLetter(LastName)}";
    }

    /// <summary>
    /// Method which capitalize first letter of the name ond last name
    /// </summary>
    private string CapitalizeFirstLetter(string input)
    {
        var postFix = input.Substring(1, input.Length - 1);
        var result = input.Remove(1, input.Length - 1)
            .ToUpper()
            .Insert(1, postFix);
        return result;
    }
}
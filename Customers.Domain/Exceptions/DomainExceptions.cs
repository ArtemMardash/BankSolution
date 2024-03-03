namespace Customers.Domain.Exceptions;

/// <summary>
/// Class ti throw an exception
/// </summary>
internal static class DomainExceptions
{
    /// <summary>
    /// Method to throw an exception
    /// </summary>
    public static void ThrowException<T>(string message, Exception? inner = null, params string[] args)
        where T : DomainException, new()
    {
        var exception = new T();
        exception.Message = message;
        exception.InnerException = inner;
        throw exception;
    }
}

/// <summary>
/// Return a message and inner exception
/// </summary>
internal class DomainException : Exception
{
    /// <summary>
    /// Message about exception
    /// </summary>
    public new string Message { get; set; }

    /// <summary>
    /// Inner exception
    /// </summary>
    public new Exception? InnerException { get; set; }

    /// <summary>
    /// Return a message and inner exception
    /// </summary>
    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Return a message and inner exception
    /// </summary>
    public DomainException(string message, params string[] args)
        : base(message)
    {
    }

    /// <summary>
    /// Return a message and inner exception
    /// </summary>
    public DomainException()
    {
    }
}

/// <summary>
/// Throw an exception if id id invalid
/// </summary>
internal class InvalidIdException : DomainException
{
    /// <summary>
    /// Throw an exception if id id invalid
    /// </summary>
    public InvalidIdException(string message, params string[] args) : base(message, args)
    {
    }

    public InvalidIdException()
    {
    }
}

/// <summary>
/// Throw an exception if contacts are invalid
/// </summary>
internal class InvalidContactsException : DomainException
{
    /// <summary>
    /// Throw an exception if contacts are invalid
    /// </summary>
    public InvalidContactsException(string message, Exception inner, params string[] args) : base(message, inner)
    {
    }

    public InvalidContactsException()
    {
    }
}

/// <summary>
/// Throw an exception if address is invalid
/// </summary>
internal class InvalidAddressException : DomainException
{
    /// <summary>
    /// Throw an exception if address is invalid
    /// </summary>
    public InvalidAddressException(string message, params string[] args) : base(message, args)
    {
    }

    public InvalidAddressException()
    {
    }
}

/// <summary>
/// Throw an exception if string is invalid
/// </summary>
internal class InvalidStringValueException : DomainException
{
    /// <summary>
    /// Throw an exception if string is invalid
    /// </summary>
    public InvalidStringValueException(string message, params string[] args) : base(message, args)
    {
    }

    public InvalidStringValueException()
    {
    }
}

/// <summary>
/// Throw an exception if email is invalid
/// </summary>
internal class InvalidEmailException : DomainException
{
    /// <summary>
    /// Throw an exception if email is invalid
    /// </summary>
    public InvalidEmailException(string message, params string[] args) : base(message, args)
    {
    }

    public InvalidEmailException()
    {
    }
}

/// <summary>
/// Throw an exception if full name is invalid 
/// </summary>
internal class InvalidFullNameException : DomainException
{
    /// <summary>
    /// Throw an exception if full name is invalid 
    /// </summary>
    public InvalidFullNameException(string message, Exception innerException, params string[] args) : base(message,
        innerException)
    {
    }

    public InvalidFullNameException()
    {
    }
}
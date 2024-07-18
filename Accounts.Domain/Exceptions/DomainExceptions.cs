namespace Accounts.Domain.Exceptions;

internal static class DomainExceptions
{
    public static void ThrowException<T>(string message, Exception? inner = null, params string[] args)
        where T : DomainException, new()
    {
        var exception = new T();
        exception.Message = message;
        exception.InnerException = inner;
        throw exception;
    }
}

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
    /// Throw an exception if id is invalid
    /// </summary>
    public InvalidIdException(string message, params string[] args) : base(message, args)
    {
    }

    public InvalidIdException()
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

internal class InvalidAmountException: DomainException
{
    public InvalidAmountException(string message, params string[] args): base(message, args)
    {

    }

    public InvalidAmountException()
    {
        
    }
}

internal class InvalidAccountStatus: DomainException
{
    public InvalidAccountStatus(string message, params string[] args): base(message, args)
    {

    }

    public InvalidAccountStatus()
    {
        
    }
}
internal class InvalidStatusToChangeBalance: DomainException
{
    public InvalidStatusToChangeBalance(string message, params string[] args): base(message, args)
    {

    }

    public InvalidStatusToChangeBalance()
    {
        
    }
}


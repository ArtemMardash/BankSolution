namespace Accounts.Application.Exceptions;

internal static class ApplicationExceptions
{
    
}

internal  class ApplicationException: Exception
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
    public ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Return a message and inner exception
    /// </summary>
    public ApplicationException(string message, params string[] args)
        : base(message)
    {
    }

    /// <summary>
    /// Return a message and inner exception
    /// </summary>
    public ApplicationException()
    {
    }
}

internal class AccountNotFoundException: ApplicationException
{
    public AccountNotFoundException(string message, params string[] args): base(message, args)
    {

    }

    public AccountNotFoundException()
    {
        
    }
}




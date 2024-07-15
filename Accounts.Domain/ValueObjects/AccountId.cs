using Accounts.Domain.Exceptions;

namespace Accounts.Domain.ValueObjects;

/// <summary>
/// Account id which contains string and guid type of id
/// </summary>
public class AccountId
{
    /// <summary>
    /// Id of account Guid
    /// </summary>
    public Guid SystemId { get; private set; }
    
    /// <summary>
    /// String id of account
    /// </summary>
    public string PublicId { get; private set; }

    /// <summary>
    /// Account id which contains string and guid type of id
    /// </summary>
    public AccountId(Guid systemId, string publicId)
    {
        if (systemId == Guid.Empty)
        {
            DomainExceptions.ThrowException<InvalidIdException>("Id can not be empty", args: nameof(systemId));
        }
        SystemId = systemId;
        SetPublicId(publicId);
    }
    
    /// <summary>
    /// Account id which contains string and guid type of id
    /// </summary>
    public void SetPublicId(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new InvalidStringValueException("publicId can not be null or empty", nameof(PublicId));
        }

        PublicId = input;
    }
}
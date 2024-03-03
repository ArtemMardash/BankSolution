using Accounts.Domain.Exceptions;

namespace Accounts.Domain.ValueObjects;

public class AccountId
{
    public Guid SystemId { get; private set; }
    
    public string PublicId { get; private set; }

    public AccountId(Guid systemId, string publicId)
    {
        if (systemId == Guid.Empty)
        {
            DomainExceptions.ThrowException<InvalidIdException>("Id can not be empty", args: nameof(systemId));
        }
        SystemId = systemId;
        SetPublicId(publicId);
    }
    

    public void SetPublicId(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new InvalidStringValueException("publicId can not be null or empty", nameof(PublicId));
        }

        PublicId = input;
    }
}
using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

/// <summary>
/// Create an address
/// </summary>
public class Address
{
    private const int ZIP_CODE_LENGTH = 5;

    private const int VALUE_LENGTH = 30;

    /// <summary>
    /// address
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Zip code
    /// </summary>
    public string ZipCode { get; private set; }

    /// <summary>
    /// Create an address
    /// </summary>
    /// <param name="address"></param>
    /// <param name="zipCode"></param>
    public Address(string address, string zipCode)
    {
        SetAddress(address);
        SetZipCode(zipCode);
    }

    /// <summary>
    /// Method to set an address
    /// </summary>
    /// <param name="address"></param>
    public void SetAddress(string address)
    {
        if (string.IsNullOrEmpty(address) || address.Length > VALUE_LENGTH)
        {
            DomainExceptions.ThrowException<InvalidAddressException>("Address out of range", args: nameof(address));
        }

        Value = address;
    }

    /// <summary>
    /// Method to set zip code
    /// </summary>
    /// <param name="zipCode"></param>
    public void SetZipCode(string zipCode)
    {
        if (zipCode.Length == ZIP_CODE_LENGTH)
        {
            ZipCode = zipCode;
        }
        else
        {
            DomainExceptions.ThrowException<InvalidAddressException>("zip code out of range", args: nameof(Address));
        }
    }
}
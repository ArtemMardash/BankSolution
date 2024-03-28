using Customers.Domain.Exceptions;
using Customers.Domain.ValueObjects;
using FluentAssertions;

namespace Customers.Tests.Domain.Tests;

public class ValueObjectsTests
{
    [Theory]
    [InlineData("12345", "USA")]
    internal void Create_Address_Should_Successfull(string zipCode, string address)
    {
        var test = () =>
        {
            var adrs = new Address(address, zipCode);
            return adrs;
        };
        test.Should().NotThrow();
        var addr = test();
        addr.Should().NotBeNull();
        addr.ZipCode.Should().NotBeNull();
        addr.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData("", "123456", typeof(InvalidAddressException))]
    internal void Create_Address_Should_Fail(string address, string zipCode, Type exceptionType)
    {
        var test = () =>
        {
            var adrs = new Address(address, zipCode);
            return adrs;
        };
        Assert.Throws(exceptionType, test);
    }

    [Theory]
    [InlineData("123456789011", "123456789@.")]
    internal void Create_Contacts_Should_Successfull(string phoneNumber, string email)
    {
        var test = () =>
        {
            var electMail = new Email(email);
            var contacts = new Contacts(electMail, phoneNumber);
            return contacts;
        };
        test.Should().NotThrow();
        var contact = test();
        contact.Should().NotBeNull();
        contact.Email.Should().NotBeNull();
        contact.PhoneNumber.Should().NotBeNull();
    }

    [Theory]
    [InlineData("1234567890", "123456789@.", typeof(InvalidContactsException))]
    internal void Create_Contacts_Should_Fail(string phoneNumber, string email, Type exceptionType,
        Type? innerException = null)
    {
        var test = () =>
        {
            var electMail = new Email(email);
            var contacts = new Contacts(electMail, phoneNumber);
            return contacts;
        };
        var exception = Assert.Throws(exceptionType, test);
        if (innerException != null)
        {
            exception.InnerException.Should().BeOfType(innerException);
        }
    }

    [Theory]
    [InlineData("artem", "Mardakhaev")]
    internal void Create_FullName_Should_Successfull(string name, string secondName)
    {
        var test = () =>
        {
            var firstName = new FirstName(name);
            var lastName = new LastName(secondName);
            var fullName = new FullName(firstName, lastName);
            return fullName;
        };
        test.Should().NotThrow();
        var fullName = test();
        fullName.Should().NotBeNull();
        fullName.LastName.Should().NotBeNull();
        fullName.FirstName.Should().NotBeNull();
    }

    // [Theory]
    // [InlineData("artem", "Mardakhaev", typeof(InvalidFullNameException))]
    // internal void Create_FullName_Should_Fail(string name, string secondName, Type exceptionType)
    // {
    //     var test = () =>
    //     {
    //         var firstName = new FirstName(name);
    //         var lastName = new LastName(secondName);
    //         var fullName = new FullName(firstName, lastName);
    //         return fullName;
    //     };
    //     Assert.Throws(exceptionType, test);
    // }

    [Theory]
    [InlineData("123 123")]
    internal void Create_FullName_From_String_Should_Successfull(string input)
    {
        var test = () =>
        {
            var fullName = FullName.CreateFromString(input);
            return fullName;
        };

        test.Should().NotThrow();
        var fullName = test();
        fullName.Should().NotBeNull();
    }

    [Theory]
    [InlineData(" 1", typeof(InvalidFullNameException))]
    [InlineData("123", typeof(InvalidFullNameException))]
    [InlineData("1 ", typeof(InvalidFullNameException))]
    internal void Create_FullName_From_String_Should_Fail(string input, Type exceptionType)
    {
        var test = () =>
        {
            var fullName = FullName.CreateFromString(input);
            return fullName;
        };

        Assert.Throws(exceptionType, test);
    }
}
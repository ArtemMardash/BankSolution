using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using FluentAssertions;

namespace Customers.Tests.Domain.Tests;

public class CustomerTests
{
    [Theory]
    [InlineData("Artem Mardakhaev", "artem@.google", "+12345678912", "12345", "USA")]
    public void Create_Customer_FromString_Should_Successfull(string fullName,
        string email,
        string phoneNumber,
        string zipCode,
        string address)
    {
        var test = () =>
        {
            var customer = new Customer(
                FullName.CreateFromString(fullName),
                new Contacts(new Email(email), phoneNumber),
                new Address(address, zipCode),
                new Address(address, zipCode));
            return customer;
        };
        test.Should().NotThrow();
        var customer = test();
        customer.Should().NotBeNull();
        customer.Contacts.Should().NotBeNull();
        customer.Id.Should().NotBeEmpty();
        customer.FullName.Should().NotBeNull();
        customer.BillingAddress.Should().NotBeNull();
        customer.MailAddress.Should().NotBeNull();
        customer.BillingAddress.ZipCode.Should().BeSameAs(customer.MailAddress.ZipCode);
        customer.BillingAddress.Value.Should().BeSameAs(customer.MailAddress.Value);
    }

    [Theory]
    [InlineData("Mardakhaev", "artem@.google", "+12345678912", "12345", "USA", "62fc163d-3e1b-4440-9db1-7c88908f40a2")]
    [InlineData("Artem Mardakhaev", "artem.google", "+12345678912", "12345", "USA",
        "62fc163d-3e1b-4440-9db1-7c88908f40a2")]
    [InlineData("Artem Mardakhaev", "artem@.google", "+123456789121", "12345", "USA",
        "62fc163d-3e1b-4440-9db1-7c88908f40a2")]
    [InlineData("Artem Mardakhaev", "artem@.google", "+12345678912", "123456", "USA",
        "62fc163d-3e1b-4440-9db1-7c88908f40a2")]
    [InlineData("Artem Mardakhaev", "artem@.google", "+12345678912", "12345", "1234567890123456789012345678901",
        "62fc163d-3e1b-4440-9db1-7c88908f40a2")]
    [InlineData("", "", "", "", "", "")]
    [InlineData("Artem Mardakhaev", "artem@.google", "+12345678912", "12345", "USA",
        "00000000-0000-0000-0000-000000000000")]
    public void Create_Customer_Should_Fail(string fullName,
        string email,
        string phoneNumber,
        string zipCode,
        string address,
        string id)
    {
        var test = () =>
        {
            var customer = new Customer(
                Guid.Parse(id),
                FullName.CreateFromString(fullName),
                new Contacts(new Email(email), phoneNumber),
                new Address(address, zipCode),
                new Address(address, zipCode));
            return customer;
        };

        test.Should().Throw<Exception>();
    }
}
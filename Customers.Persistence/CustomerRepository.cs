using System.Net.Mail;
using System.Text;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using Customers.Persistence.AppContext;
using Customers.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence;

/// <summary>
/// Realization of all methods of ICustomerRepository
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;

    /// <summary>
    /// Realization of all methods of ICustomerRepository
    /// </summary>
    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Method to get data about customer
    /// </summary>
    public async Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var cust = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (cust is null)
        {
            return null;
        }

        var mailAddress = cust.MailAddress.Split(",");
        var billingAddress = cust.BillingAddress.Split(",");
        var contacts = new Contacts(new Email(cust.Contacts.Email), cust.Contacts.PhoneNumber);
        var customer = new Customer(cust.Id,
            FullName.CreateFromString(cust.FullName),
            contacts, new Address(mailAddress[1], mailAddress[0]),
            new Address(billingAddress[1], billingAddress[0]));
        return customer;
    }

    /// <summary>
    /// Method to add a new customer to db
    /// </summary>
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        var customerDb = new CustomerDb
        {
            Id = customer.Id,
            BillingAddress = $"{customer.BillingAddress.ZipCode},{customer.BillingAddress.Value}",
            Contacts = new ContactsDb
            {
                Email = customer.Contacts.Value.Value,
                PhoneNumber = customer.Contacts.PhoneNumber
            },
            FullName = customer.FullName.GetString(),
            MailAddress = $"{customer.MailAddress.ZipCode},{customer.MailAddress.Value}"
        };
        await _context.Customers.AddAsync(customerDb, cancellationToken);
    }
}
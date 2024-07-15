using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Events;
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
                Email = customer.Contacts.Email.Value,
                PhoneNumber = customer.Contacts.PhoneNumber
            },
            FullName = customer.FullName.GetString(),
            MailAddress = $"{customer.MailAddress.ZipCode},{customer.MailAddress.Value}",
        };

        customerDb.DomainEvents.AddRange(customer.DomainEvents);

        await _context.Customers.AddAsync(customerDb, cancellationToken);
    }

    /// <summary>
    /// Method to delete customer
    /// </summary>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await GetAsync(id, cancellationToken);
        var customerDb = _context.ChangeTracker.Entries<CustomerDb>().FirstOrDefault(c => c.Entity.Id == customer.Id)
            .Entity ?? new CustomerDb
        {
            Id = customer.Id,
            BillingAddress = $"{customer.BillingAddress.ZipCode},{customer.BillingAddress.Value}",
            Contacts = new ContactsDb
            {
                Email = customer.Contacts.Email.Value,
                PhoneNumber = customer.Contacts.PhoneNumber
            },
            FullName = customer.FullName.GetString(),
            MailAddress = $"{customer.MailAddress.ZipCode},{customer.MailAddress.Value}",
        };
        customerDb.DomainEvents.Add(new CustomerDeleted { Id = id });
        _context.Customers.Remove(customerDb);
    }

    public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        var customersDb = await _context.Customers.ToListAsync();
        return customersDb.Select(cdb => DbToDomain(cdb)).ToList();

    }

    private Customer DbToDomain(CustomerDb customerDb)
    {
        var mailAddress = customerDb.MailAddress.Split(",");
        var billingAddress = customerDb.BillingAddress.Split(",");
        var contacts = new Contacts(new Email(customerDb.Contacts.Email), customerDb.Contacts.PhoneNumber);
        var customer = new Customer(customerDb.Id,
            FullName.CreateFromString(customerDb.FullName),
            contacts, new Address(mailAddress[1], mailAddress[0]),
            new Address(billingAddress[1], billingAddress[0]));
        return customer;
    }
}
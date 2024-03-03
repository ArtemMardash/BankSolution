using Customers.Application.Dto_s;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;

namespace Customers.Application.UseCases;

public class AddCustomerUseCase : IAddCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _context;

    public AddCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork context )
    {
        _customerRepository = customerRepository;
        _context = context;
    }

    public async Task<Guid> ExcexuteAsync(CreateCustomerDto dto, CancellationToken cancellationToken)
    {
        var customer = new Customer(
            new FullName(new FirstName(dto.FirstName), new LastName(dto.LastName)),
            new Contacts(new Email(dto.Contacts.Email), dto.Contacts.PhoneNumber),
            new Address(dto.MailAddress.Address, dto.MailAddress.ZipCode),
            new Address(dto.BillingAddress.Address, dto.BillingAddress.ZipCode)
        );
        await _customerRepository.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync();
        return customer.Id;
    }
}
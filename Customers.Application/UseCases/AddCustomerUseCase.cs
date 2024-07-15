using Customers.Application.Dtos;
using Customers.Application.Dtos.Responses;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Events;
using Customers.Domain.ValueObjects;
using MediatR;

namespace Customers.Application.UseCases;

public class AddCustomerUseCase : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _context;
    private readonly IMediator _mediator;

    public AddCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork context, IMediator mediator)
    {
        _customerRepository = customerRepository;
        _context = context;
        _mediator = mediator;
    }
    

    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        
        var customer = new Customer(
            new FullName(new FirstName(request.FirstName), new LastName(request.LastName)),
            new Contacts(new Email(request.Contacts.Email), request.Contacts.PhoneNumber),
            new Address(request.MailAddress.Address, request.MailAddress.ZipCode),
            new Address(request.BillingAddress.Address, request.BillingAddress.ZipCode)
        );
        await _customerRepository.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateCustomerResponse
        {
            Id = customer.Id
        };
    }
    
}
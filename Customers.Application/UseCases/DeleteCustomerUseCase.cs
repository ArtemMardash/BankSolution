using Customers.Application.Dtos;
using Customers.Application.Interfaces;
using MediatR;

namespace Customers.Application.UseCases;

public class DeleteCustomerUseCase: IRequestHandler<DeleteCustomerRequest>
{
    private readonly ICustomerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerUseCase(ICustomerRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
       await _repository.DeleteAsync(request.Id, cancellationToken);
       await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
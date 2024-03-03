using Customers.Application.Dto_s;

namespace Customers.Application.UseCases;

public interface IAddCustomerUseCase
{
    public Task<Guid> ExcexuteAsync(CreateCustomerDto dto, CancellationToken cancellationToken);
}
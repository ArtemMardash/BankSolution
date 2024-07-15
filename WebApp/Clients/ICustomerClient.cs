using Refit;
using WebApp.Components.Pages;

namespace WebApp.Clients;

public interface ICustomerClient
{
    [Get("/api/customers")]
    public Task<CustomersResponse> GetCustomersAsync();

    [Get("/api/customer/{id}")]
    public Task<Customer> GetCustomerAsync(Guid id);

    [Post("/api/customer")]
    public Task<CreateCustomerResponse> CreateCustomerAsync([Body]CreateCustomerRequest request);
}

public class CustomersResponse
{
    public List<Customer> Customers { get; set; }
}

public class CreateCustomerResponse
{
    public Guid Id { get; set; }
}

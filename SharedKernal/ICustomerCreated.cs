namespace SharedKernal;

public interface ICustomerCreated
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Guid Id { get; set; }
}
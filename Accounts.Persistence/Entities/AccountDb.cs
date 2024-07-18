namespace Accounts.Persistence.Entities;

public class AccountDb: BaseEntity
{
    /// <summary>
    /// Id of account
    /// </summary>
    public Guid Id { get; set; }
    
    public string PublicId { get; set; }
    
    /// <summary>
    /// Id of customer
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Balance in the account
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Status of account
    /// </summary>
    public int Status { get; set; }

}
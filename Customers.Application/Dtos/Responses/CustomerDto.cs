using MediatR;

namespace Customers.Application.Dtos.Responses;

public class CustomerDto
{
    /// <summary>
    /// Id of customer
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Full name of customer
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Customer's email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// mail address of the customer
    /// </summary>
    public string MailAddress { get; set; }

    /// <summary>
    /// billing address of the customer
    /// </summary>
    public string BillingAddress { get; set; }
}
namespace TokanPages.Backend.Application.Revenue.Commands;

public class AddSubscriptionCommandResult
{
    public Guid UserId { get; set; }

    public Guid SubscriptionId { get; set; }

    public string? ExtOrderId { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
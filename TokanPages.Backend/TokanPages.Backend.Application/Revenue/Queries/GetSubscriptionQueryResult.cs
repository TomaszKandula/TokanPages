using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Subscriptions.Queries;

public class GetSubscriptionQueryResult
{
    public Guid UserId { get; set; }

    public bool IsActive { get; set; }

    public bool AutoRenewal { get; set; }

    public TermType Term { get; set; }

    public decimal TotalAmount { get; set; }

    public string? CurrencyIso { get; set; }

    public string? ExtCustomerId { get; set; }

    public string? ExtOrderId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
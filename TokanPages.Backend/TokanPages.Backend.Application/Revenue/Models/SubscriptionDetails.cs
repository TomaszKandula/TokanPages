using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models;

[ExcludeFromCodeCoverage]
public class SubscriptionDetails
{
    public bool IsActive { get; set; }

    public bool AutoRenewal { get; set; }

    public decimal TotalAmount { get; set; }

    public string? CurrencyIso { get; set; }

    public string? ExtCustomerId { get; set; }

    public string? ExtOrderId { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
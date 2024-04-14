using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Revenue.Dto.Subscriptions;

/// <summary>
/// Use it when you want to update existing user subscription.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateSubscriptionDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Unique order ID issued by the system.
    /// </summary>
    public string? ExtOrderId { get; set; }

    /// <summary>
    /// Auto renewal flag.
    /// </summary>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public bool? AutoRenewal { get; set; }

    /// <summary>
    /// Selected payment term.
    /// </summary>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public TermType? Term { get; set; }

    /// <summary>
    /// Subscription price amount.
    /// </summary>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public decimal? TotalAmount { get; set; }

    /// <summary>
    /// Amount currency ISO code (three letters only).
    /// </summary>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public string? CurrencyIso { get; set; }
}
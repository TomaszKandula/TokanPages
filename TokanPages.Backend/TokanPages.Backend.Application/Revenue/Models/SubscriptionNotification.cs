using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models;

[ExcludeFromCodeCoverage]
public class SubscriptionNotification : SubscriptionDetails
{
    public string? PaymentStatus { get; set; }
}
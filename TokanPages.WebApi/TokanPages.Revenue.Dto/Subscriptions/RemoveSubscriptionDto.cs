using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Subscriptions;

/// <summary>
/// Use it when you want to remove user subscription.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveSubscriptionDto
{
    /// <summary>
    /// Optional value.
    /// </summary>
    public Guid? UserId { get; set; }
}
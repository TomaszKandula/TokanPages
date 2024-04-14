using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Revenue.Dto.Subscriptions;

/// <summary>
/// Subscription model.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddSubscriptionDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Selected payment term.
    /// </summary>
    public TermType SelectedTerm { get; set; }

    /// <summary>
    /// User currency.
    /// </summary>
    public string? UserCurrency { get; set; }

    /// <summary>
    /// User language.
    /// </summary>
    public string? UserLanguage { get; set; }
}
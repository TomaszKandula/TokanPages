using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Buyer details.
/// </summary>
[ExcludeFromCodeCoverage]
public class BuyerDto
{
    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Phone.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// FirstName.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Language.
    /// </summary>
    public string? Language { get; set; }
}
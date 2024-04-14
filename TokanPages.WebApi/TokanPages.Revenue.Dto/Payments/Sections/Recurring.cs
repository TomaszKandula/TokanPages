using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Recurring.
/// </summary>
[ExcludeFromCodeCoverage]
public class Recurring
{
    /// <summary>
    /// Frequency.
    /// </summary>
    public int? Frequency { get; set; }

    /// <summary>
    /// Expiry.
    /// </summary>
    public string? Expiry { get; set; }
}
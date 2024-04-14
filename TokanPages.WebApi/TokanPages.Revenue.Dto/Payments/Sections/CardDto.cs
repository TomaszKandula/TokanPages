using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Credit card definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class CardDto
{
    /// <summary>
    /// Card number.
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Card expiration month.
    /// </summary>
    public string? ExpirationMonth { get; set; }

    /// <summary>
    /// Card expiration year.
    /// </summary>
    public string? ExpirationYear { get; set; }

    /// <summary>
    /// CVV. 
    /// </summary>
    public string? Cvv { get; set; }
}
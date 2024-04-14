using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Payment details.
/// </summary>
[ExcludeFromCodeCoverage]
public class PayMethodDto
{
    /// <summary>
    /// Card details.
    /// </summary>
    public CardDto? Card { get; set; }
    
    /// <summary>
    /// Type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Value.
    /// </summary>
    public string? Value { get; set; }
}
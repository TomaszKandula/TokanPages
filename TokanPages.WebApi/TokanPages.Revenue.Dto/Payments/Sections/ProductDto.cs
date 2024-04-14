using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Product details.
/// </summary>
[ExcludeFromCodeCoverage]
public class ProductDto
{
    /// <summary>
    /// Name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// UnitPrice.
    /// </summary>
    public string? UnitPrice { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public string? Quantity { get; set; }
}
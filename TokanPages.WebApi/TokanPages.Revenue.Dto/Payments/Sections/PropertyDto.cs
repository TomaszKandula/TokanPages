using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Property details.
/// </summary>
[ExcludeFromCodeCoverage]
public class PropertyDto
{
    /// <summary>
    /// Name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Value.
    /// </summary>
    public string? Value { get; set; }
}
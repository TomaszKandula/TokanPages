using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// 3DS authentication.
/// </summary>
[ExcludeFromCodeCoverage]
public class Authentication
{
    /// <summary>
    /// Recurring.
    /// </summary>
    public Recurring? Recurring { get; set; }
}
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class PayByLinks
{
    public string? Value { get; set; }

    public string? BrandImageUrl { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public string? MinAmount { get; set; }

    public string? MaxAmount { get; set; }
}
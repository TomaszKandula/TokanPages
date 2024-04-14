using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class PexTokens
{
    public string? AccountNumber { get; set; }

    public string? PayType { get; set; }

    public string? Value { get; set; }

    public string? Name { get; set; }

    public string? BrandImageUrl { get; set; }

    public string? Preferred { get; set; }

    public string? Status { get; set; }
}
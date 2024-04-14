using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models.Sections;

[ExcludeFromCodeCoverage]
public class CardTokens
{
    public string? CardExpirationYear { get; set; }

    public string? CardExpirationMonth { get; set; }

    public string? CardNumberMasked { get; set; }

    public string? CardScheme { get; set; }

    public string? Value { get; set; }

    public string? BrandImageUrl { get; set; }

    public string? Preferred { get; set; }

    public string? Status { get; set; }
}
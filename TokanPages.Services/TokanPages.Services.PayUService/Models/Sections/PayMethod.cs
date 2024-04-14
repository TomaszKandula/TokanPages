using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class PayMethod
{
    public Card? Card { get; set; }

    public string? Type { get; set; }

    public string? Value { get; set; }
}
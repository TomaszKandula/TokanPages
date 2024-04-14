using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class GenerateCardTokenOutput
{
    public string? Value { get; set; }

    public string? MaskedCard { get; set; }
}
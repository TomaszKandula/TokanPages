using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class GenerateCardTokenInput
{
    public string? PosId { get; set; }

    public string? Type { get; set; }

    public Card? Card { get; set; }
}
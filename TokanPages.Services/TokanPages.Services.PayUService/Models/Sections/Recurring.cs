using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Recurring
{
    public int? Frequency { get; set; }

    public string? Expiry { get; set; }
}
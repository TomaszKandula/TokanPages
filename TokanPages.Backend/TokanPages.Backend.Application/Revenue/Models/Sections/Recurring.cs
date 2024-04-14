using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models.Sections;

[ExcludeFromCodeCoverage]
public class Recurring
{
    public int? Frequency { get; set; }

    public string? Expiry { get; set; }
}
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Currencies.Queries.Models;

[ExcludeFromCodeCoverage]
public class CurrencyCode
{
    public int SystemCode { get; set; }

    public string Currency { get; set; } = "";
}
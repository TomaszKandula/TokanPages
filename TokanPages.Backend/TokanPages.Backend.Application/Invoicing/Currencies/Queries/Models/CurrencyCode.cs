using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Currencies.Queries.Models;

[ExcludeFromCodeCoverage]
public class CurrencyCode
{
    public int SystemCode { get; set; }

    public string Currency { get; set; } = "";
}
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQueryResult
{
    public int SystemCode { get; set; }

    public string Country { get; set; } = "";
}
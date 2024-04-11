using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQueryResult
{
    public int SystemCode { get; set; }

    public string Country { get; set; } = "";
}
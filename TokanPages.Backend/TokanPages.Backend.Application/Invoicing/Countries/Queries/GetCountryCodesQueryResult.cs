using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQueryResult : BaseResponse
{
    public string Country { get; set; } = "";
}
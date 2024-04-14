using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Currencies.Queries;

[ExcludeFromCodeCoverage]
public class GetCurrencyCodesQueryResult : BaseResponse
{
    public string Currency { get; set; } = "";    
}
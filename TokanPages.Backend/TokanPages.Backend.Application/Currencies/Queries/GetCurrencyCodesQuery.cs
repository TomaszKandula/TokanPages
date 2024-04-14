using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Currencies.Queries;

[ExcludeFromCodeCoverage]
public class GetCurrencyCodesQuery : IRequest<IList<GetCurrencyCodesQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
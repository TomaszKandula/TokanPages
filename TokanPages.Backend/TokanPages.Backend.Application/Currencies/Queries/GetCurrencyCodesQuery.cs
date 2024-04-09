using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Currencies.Queries;

[ExcludeFromCodeCoverage]
public class GetCurrencyCodesQuery : IRequest<IEnumerable<GetCurrencyCodesQueryResult>>
{
    public string FilterBy { get; set; }
}
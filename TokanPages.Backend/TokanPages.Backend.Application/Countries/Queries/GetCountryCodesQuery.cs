using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQuery : IRequest<IEnumerable<GetCountryCodesQueryResult>>
{
    public string FilterBy { get; set; }
}
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQuery : IRequest<IList<GetCountryCodesQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
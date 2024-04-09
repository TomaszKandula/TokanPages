using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeListQuery : IRequest<IEnumerable<GetPaymentTypeListQueryResult>>
{
    public string FilterBy { get; set; }
}
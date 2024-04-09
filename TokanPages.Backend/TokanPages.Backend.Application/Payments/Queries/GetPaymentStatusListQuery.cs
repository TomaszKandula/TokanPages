using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusListQuery : IRequest<IEnumerable<GetPaymentStatusListQueryResult>>
{
    public string FilterBy { get; set; }
}
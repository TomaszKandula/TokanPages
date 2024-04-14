using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusListQuery : IRequest<IList<GetPaymentStatusListQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
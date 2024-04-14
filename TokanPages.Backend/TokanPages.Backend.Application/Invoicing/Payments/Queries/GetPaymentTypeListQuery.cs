using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeListQuery : IRequest<IList<GetPaymentTypeListQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
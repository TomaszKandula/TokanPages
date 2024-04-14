using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusQuery : IRequest<IList<GetPaymentStatusQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeQuery : IRequest<IList<GetPaymentTypeQueryResult>>
{
    public string FilterBy { get; set; } = "";
}
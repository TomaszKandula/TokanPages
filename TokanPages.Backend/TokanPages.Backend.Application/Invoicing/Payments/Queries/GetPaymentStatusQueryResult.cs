using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusQueryResult : BaseResponse
{
    public string PaymentStatus { get; set; } = "";
}
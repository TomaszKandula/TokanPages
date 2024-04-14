using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeQueryResult : BaseResponse
{
    public string PaymentType { get; set; } = "";
}
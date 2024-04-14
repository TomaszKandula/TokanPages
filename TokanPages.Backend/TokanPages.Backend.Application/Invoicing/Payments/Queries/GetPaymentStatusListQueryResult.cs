using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusListQueryResult
{
    public int SystemCode { get; set; }

    public string PaymentStatus { get; set; } = "";
}
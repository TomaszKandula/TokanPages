using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeListQueryResult
{
    public int SystemCode { get; set; }

    public string PaymentType { get; set; } = "";
}
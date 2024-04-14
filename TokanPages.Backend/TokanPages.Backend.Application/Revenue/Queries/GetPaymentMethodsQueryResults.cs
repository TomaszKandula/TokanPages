using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetPaymentMethodsQueryResults
{
    public IEnumerable<CardTokens>? CardTokens { get; set; }

    public IEnumerable<PexTokens>? PexTokens { get; set; }

    public IEnumerable<PayByLinks>? PayByLinks { get; set; }

    public Status? Status { get; set; }
}
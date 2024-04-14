using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class PaymentMethodsOutput
{
    public IEnumerable<CardTokens>? CardTokens { get; set; }

    public IEnumerable<PexTokens>? PexTokens { get; set; }

    public IEnumerable<PayByLinks>? PayByLinks { get; set; }

    public Status? Status { get; set; }
}
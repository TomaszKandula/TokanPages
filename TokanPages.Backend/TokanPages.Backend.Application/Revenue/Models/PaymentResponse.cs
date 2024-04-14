using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Models;

[ExcludeFromCodeCoverage]
public class PaymentResponse
{
    public string? OrderId { get; set; }

    public PayMethods? PayMethods { get; set; }

    public Status? Status { get; set; }
}
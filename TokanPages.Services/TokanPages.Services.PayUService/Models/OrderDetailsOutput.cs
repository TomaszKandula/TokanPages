using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class OrderDetailsOutput
{
    public IEnumerable<Order>? Orders { get; set; }

    public Status? Status { get; set; }

    public IEnumerable<Property>? Properties { get; set; }
}
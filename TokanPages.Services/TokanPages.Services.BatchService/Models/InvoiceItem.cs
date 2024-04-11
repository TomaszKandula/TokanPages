using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceItem : InvoiceItemBase
{
    public decimal ValueAmount { get; set; }

    public decimal GrossAmount { get; set; }
}
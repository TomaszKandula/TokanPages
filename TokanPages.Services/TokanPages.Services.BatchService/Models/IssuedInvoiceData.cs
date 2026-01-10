using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class IssuedInvoiceData
{
    public string InvoiceContent { get; set; } = "";

    public BatchInvoice CurrentInvoice { get; set; } = new();

    public ICollection<IssuedInvoice> InvoiceCollection { get; set; } = new List<IssuedInvoice>();

    public BatchInvoiceProcessing BatchInvoiceProcessing { get; set; } = new();

    public Stopwatch ProcessingTimer { get; set; } = new();
}
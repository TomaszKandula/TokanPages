using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class IssuedInvoiceData
{
    public string InvoiceContent { get; set; } = "";

    public BatchInvoices CurrentInvoice { get; set; } = new();

    public ICollection<IssuedInvoices> InvoiceCollection { get; set; } = new List<IssuedInvoices>();

    public BatchInvoicesProcessing BatchInvoicesProcessing { get; set; } = new();

    public Stopwatch ProcessingTimer { get; set; } = new();
}
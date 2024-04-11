using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class ProcessingError
{
    public string Error { get; set; } = "";

    public string InnerError { get; set; } = "";

    public string InvoiceNumber { get; set; } = "";

    public Stopwatch Timer { get; set; } = new();

    public BatchInvoicesProcessing ProcessingObject { get; set; } = new();
}
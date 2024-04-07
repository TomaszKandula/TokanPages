using TokanPages.Services.BatchService.Models;

namespace TokanPages.Services.BatchService;

public interface IBatchService
{
    Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails, CancellationToken cancellationToken = default);

    Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default);

    Task<ProcessingStatus> GetBatchInvoiceProcessingStatus(Guid processBatchKey, CancellationToken cancellationToken = default);
 
    Task<InvoiceData> GetIssuedInvoice(string invoiceNumber, CancellationToken cancellationToken = default);
}
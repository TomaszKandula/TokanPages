using TokanPages.Services.BatchService.Models;

namespace TokanPages.Services.BatchService;

public interface IBatchService
{
    Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails);

    Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default);

    //TODO: to be removed
    Task<ProcessingStatus> GetBatchInvoiceProcessingStatus(Guid processBatchKey, CancellationToken cancellationToken = default);
 
    //TODO: to be removed
    Task<InvoiceData> GetIssuedInvoice(string invoiceNumber, CancellationToken cancellationToken = default);
}
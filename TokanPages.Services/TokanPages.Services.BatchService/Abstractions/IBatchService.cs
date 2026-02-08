using TokanPages.Services.BatchService.Models;

namespace TokanPages.Services.BatchService.Abstractions;

public interface IBatchService
{
    Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails);

    Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default);
}
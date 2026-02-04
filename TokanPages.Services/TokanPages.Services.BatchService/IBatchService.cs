using TokanPages.Services.BatchService.Models;

namespace TokanPages.Services.BatchService;

public interface IBatchService
{
    Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails);

    Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default);
}
using System.Diagnostics;
using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.BatchService.Abstractions;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

public class OrderBatchProcessingCommandHandler : RequestHandler<OrderBatchProcessingCommand, Unit>
{
    private readonly IBatchService _batchService;

    public OrderBatchProcessingCommandHandler(ILoggerService loggerService, IBatchService batchService) : base(loggerService) => _batchService = batchService;

    public override async Task<Unit> Handle(OrderBatchProcessingCommand request, CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();

        timer.Start();
        LoggerService.LogInformation("Starting the processing of outstanding invoices...");
        await _batchService.ProcessOutstandingInvoices(cancellationToken);
        timer.Stop();
        LoggerService.LogInformation($"All outstanding invoices has been processed within: {timer.Elapsed}.");

        return Unit.Value;
    }
}
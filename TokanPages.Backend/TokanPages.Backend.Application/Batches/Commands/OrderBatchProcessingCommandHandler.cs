using System.Diagnostics;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.BatchService;

namespace TokanPages.Backend.Application.Batches.Commands;

public class OrderBatchProcessingCommandHandler : RequestHandler<OrderBatchProcessingCommand, Unit>
{
    private readonly IBatchService _batchService;

    public OrderBatchProcessingCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IBatchService batchService) 
        : base(databaseContext, loggerService) => _batchService = batchService;

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
using System.Diagnostics;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.BatchService.Abstractions;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

public class OrderBatchProcessingCommandHandler : RequestHandler<OrderBatchProcessingCommand, Unit>
{
    private readonly IBatchService _batchService;

    public OrderBatchProcessingCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IBatchService batchService) 
        : base(operationDbContext, loggerService) => _batchService = batchService;

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
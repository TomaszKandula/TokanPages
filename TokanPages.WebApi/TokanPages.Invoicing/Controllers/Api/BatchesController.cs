using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Invoicing.Batches.Commands;
using TokanPages.Backend.Application.Invoicing.Batches.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Invoicing.Controllers.Mappers;
using TokanPages.Invoicing.Dto.Batches;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for batch invoicing.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class BatchesController : ApiBaseController
{
    /// <summary>
    /// Batches controller.
    /// </summary>
    /// <param name="mediator"></param>
    public BatchesController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Allows to post new invoices to be processed and issued.
    /// </summary>
    /// <param name="payload">Invoice details.</param>
    /// <returns>Process batch key.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(OrderInvoiceBatchCommandResult), StatusCodes.Status200OK)]
    public async Task<OrderInvoiceBatchCommandResult> OrderInvoiceBatch([FromBody] OrderInvoiceBatchDto payload) 
        => await Mediator.Send(BatchMapper.MapToOrderInvoiceBatchCommandRequest(payload));

    /// <summary>
    /// Allows to start batch processing of ordered invoices.
    /// </summary>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> OrderBatchProcessing() 
        => await Mediator.Send(new OrderBatchProcessingCommand());

    /// <summary>
    /// Returns processing status. 
    /// </summary>
    /// <param name="processBatchKey">Unique key.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetBatchProcessingStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetBatchProcessingStatusQueryResult> GetBatchProcessingStatus([FromQuery] Guid processBatchKey) 
        => await Mediator.Send(new GetBatchProcessingStatusQuery { ProcessBatchKey = processBatchKey });

    /// <summary>
    /// Returns issued invoice.
    /// </summary>
    /// <param name="invoiceNumber">Invoice number.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<FileContentResult> GetIssuedBatchInvoice([FromQuery] string invoiceNumber) 
        => await Mediator.Send(new GetIssuedBatchInvoiceQuery { InvoiceNumber = invoiceNumber });

    /// <summary>
    /// Returns current batch processing statuses.
    /// </summary>
    /// <returns>List of processes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBatchProcessingStatusesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetBatchProcessingStatusesQueryResult>> GetBatchProcessingStatuses() 
        => await Mediator.Send(new GetBatchProcessingStatusesQuery { FilterBy = string.Empty });

    /// <summary>
    /// Returns data for given batch processing status.
    /// </summary>
    /// <param name="status">New, started, finished, failed.</param>
    /// <returns>List of processes.</returns>
    [HttpGet("{status}")]
    [ProducesResponseType(typeof(IEnumerable<GetBatchProcessingStatusesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetBatchProcessingStatusesQueryResult>> GetBatchProcessingStatusCode([FromRoute] string status) 
        => await Mediator.Send(new GetBatchProcessingStatusesQuery { FilterBy = status });
}
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Batches.Commands;
using TokanPages.Backend.Application.Batches.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Invoicing.Controllers.Mappers;
using TokanPages.Invoicing.Dto.Batches;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for batch invoicing.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching.
/// </remarks>
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
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(OrderInvoiceBatchCommandResult), StatusCodes.Status200OK)]
    public async Task<OrderInvoiceBatchCommandResult> OrderInvoiceBatch([FromBody] OrderInvoiceBatchDto payload) 
        => await Mediator.Send(BatchMapper.MapToOrderInvoiceBatchCommandRequest(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> OrderBatchProcessing() 
        => await Mediator.Send(new OrderBatchProcessingCommand());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="processBatchKey"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetBatchProcessingStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetBatchProcessingStatusQueryResult> GetBatchProcessingStatus([FromQuery] Guid processBatchKey) 
        => await Mediator.Send(new GetBatchProcessingStatusQuery { ProcessBatchKey = processBatchKey });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invoiceNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<FileContentResult> GetIssuedBatchInvoice([FromQuery] string invoiceNumber) 
        => await Mediator.Send(new GetIssuedBatchInvoiceQuery { InvoiceNumber = invoiceNumber });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBatchProcessingStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetBatchProcessingStatusListQueryResult>> GetBatchProcessingStatusList() 
        => await Mediator.Send(new GetBatchProcessingStatusListQuery { FilterBy = string.Empty });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet("{status}")]
    [ProducesResponseType(typeof(IEnumerable<GetBatchProcessingStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetBatchProcessingStatusListQueryResult>> GetBatchProcessingStatusCode([FromRoute] string status) 
        => await Mediator.Send(new GetBatchProcessingStatusListQuery { FilterBy = status });
}
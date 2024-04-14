using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Invoicing.Payments.Queries;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for invoice payments.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class PaymentsController : ApiBaseController
{
    private readonly IPaymentsCache _paymentsCache;

    /// <summary>
    /// Payments Controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="paymentsCache">REDIS cache instance.</param>
    public PaymentsController(IMediator mediator, IPaymentsCache paymentsCache) 
        : base(mediator) => _paymentsCache = paymentsCache;

    /// <summary>
    /// Returns list of payment types, i.e: credit card.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Payment types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetPaymentTypeListQueryResult>> GetPaymentTypeList([FromQuery] bool noCache = false) 
        => await _paymentsCache.GetPaymentTypeList(string.Empty, noCache);

    /// <summary>
    /// Returns given payment type.
    /// </summary>
    /// <param name="type">Payment type, i.e: credit card.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Payment type code, i.e: 1</returns>
    [HttpGet("{type}")]
    [ProducesResponseType(typeof(IList<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetPaymentTypeListQueryResult>> GetPaymentType([FromRoute] string type, [FromQuery] bool noCache = false) 
        => await _paymentsCache.GetPaymentTypeList(type, noCache);

    /// <summary>
    /// Returns payment statuses, i.e: unpaid.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Payment status.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusList([FromQuery] bool noCache = false)
        => await _paymentsCache.GetPaymentStatusList(string.Empty, noCache);

    /// <summary>
    /// Returns payment status code for given status.
    /// </summary>
    /// <param name="status">Payment status, i.e: unpaid.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Payment status code, i.e: 1.</returns>
    [HttpGet("{status}")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusCode([FromRoute] string status, [FromQuery] bool noCache = false)
        => await _paymentsCache.GetPaymentStatusList(status, noCache);
}
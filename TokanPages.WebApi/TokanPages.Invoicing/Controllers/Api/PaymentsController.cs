using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Payments.Queries;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for invoice payments.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class PaymentsController : ApiBaseController
{
    /// <summary>
    /// Payments Controller.
    /// </summary>
    /// <param name="mediator"></param>
    public PaymentsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns list of payment types, i.e: credit card.
    /// </summary>
    /// <returns>Payment types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentTypeListQueryResult>> GetPaymentTypeList() 
        => await Mediator.Send(new GetPaymentTypeListQuery { FilterBy = string.Empty });

    /// <summary>
    /// Returns given payment type.
    /// </summary>
    /// <param name="type">Payment type, i.e: credit card</param>
    /// <returns>Payment type code, i.e: 1</returns>
    [HttpGet("{type}")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentTypeListQueryResult>> GetPaymentType([FromRoute] string type) 
        => await Mediator.Send(new GetPaymentTypeListQuery { FilterBy = type });

    /// <summary>
    /// Returns payment statuses, i.e: unpaid.
    /// </summary>
    /// <returns>Payment status.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusList()
        => await Mediator.Send(new GetPaymentStatusListQuery { FilterBy = string.Empty });

    /// <summary>
    /// Returns payment status code for given status.
    /// </summary>
    /// <param name="status">Payment status, i.e: unpaid.</param>
    /// <returns>Payment status code, i.e: 1.</returns>
    [HttpGet("{status}")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusCode([FromRoute] string status)
        => await Mediator.Send(new GetPaymentStatusListQuery { FilterBy = status });
}
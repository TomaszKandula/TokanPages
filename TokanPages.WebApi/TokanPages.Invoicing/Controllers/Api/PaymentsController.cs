using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Payments.Queries;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for .
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class PaymentsController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public PaymentsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
     /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentTypeListQueryResult>> GetPaymentTypeList() 
        => await Mediator.Send(new GetPaymentTypeListQuery { FilterBy = string.Empty });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet("{type}")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentTypeListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentTypeListQueryResult>> GetPaymentType([FromRoute] string type) 
        => await Mediator.Send(new GetPaymentTypeListQuery { FilterBy = type });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusList([FromQuery] string status)
        => await Mediator.Send(new GetPaymentStatusListQuery { FilterBy = status });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet("{status}")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusListQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetPaymentStatusListQueryResult>> GetPaymentStatusCode([FromRoute] string status)
        => await Mediator.Send(new GetPaymentStatusListQuery { FilterBy = status });
}
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Countries.Queries;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for .
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class CountriesController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public CountriesController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetCountryCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetCountryCodesQueryResult>> GetCountryCodeList() 
        => await Mediator.Send(new GetCountryCodesQuery { FilterBy = string.Empty });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    [HttpGet("{country}")]
    [ProducesResponseType(typeof(IEnumerable<GetCountryCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetCountryCodesQueryResult>> GetCountryCode([FromRoute] string country)
        => await Mediator.Send(new GetCountryCodesQuery { FilterBy = country });
}
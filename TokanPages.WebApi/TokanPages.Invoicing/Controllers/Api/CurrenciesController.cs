using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Currencies.Queries;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for currencies data.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class CurrenciesController : ApiBaseController
{
    /// <summary>
    /// Currencies Controller.
    /// </summary>
    /// <param name="mediator"></param>
    public CurrenciesController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns list of currency codes.
    /// </summary>
    /// <returns>Currency code list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetCurrencyCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetCurrencyCodesQueryResult>> GetCurrencyCodeList() 
        => await Mediator.Send(new GetCurrencyCodesQuery { FilterBy = string.Empty });

    /// <summary>
    /// Returns currency code for given currency name. 
    /// </summary>
    /// <param name="currency">Currency name, i.e: CHF</param>
    /// <returns>Currency code, i.e: 756.</returns>
    [HttpGet("{currency}")]
    [ProducesResponseType(typeof(IEnumerable<GetCurrencyCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetCurrencyCodesQueryResult>> GetCurrencyCode([FromRoute] string currency) 
        => await Mediator.Send(new GetCurrencyCodesQuery { FilterBy = currency });
}
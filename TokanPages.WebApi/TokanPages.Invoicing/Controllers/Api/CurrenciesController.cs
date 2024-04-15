using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Invoicing.Currencies.Queries;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for currencies data.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/invoicing/[controller]/[action]")]
public class CurrenciesController : ApiBaseController
{
    private readonly ICurrenciesCache _currenciesCache;

    /// <summary>
    /// Currencies Controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="currenciesCache">REDIS cache instance.</param>
    public CurrenciesController(IMediator mediator, ICurrenciesCache currenciesCache) 
        : base(mediator) => _currenciesCache = currenciesCache;

    /// <summary>
    /// Returns list of currency codes.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Currency code list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<GetCurrencyCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetCurrencyCodesQueryResult>> GetCurrencyCodes([FromQuery] bool noCache = false) 
        => await _currenciesCache.GetCurrencyCodes(string.Empty, noCache);

    /// <summary>
    /// Returns currency code for given currency name.
    /// </summary>
    /// <param name="currency">Currency name, i.e: CHF</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Currency code, i.e: 756.</returns>
    [HttpGet("{currency}")]
    [ProducesResponseType(typeof(IList<GetCurrencyCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetCurrencyCodesQueryResult>> GetCurrencyCode([FromRoute] string currency, [FromQuery] bool noCache = false) 
        => await _currenciesCache.GetCurrencyCodes(currency, noCache);
}
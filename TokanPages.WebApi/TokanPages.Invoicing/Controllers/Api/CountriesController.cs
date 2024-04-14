using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Invoicing.Countries.Queries;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for countries data.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class CountriesController : ApiBaseController
{
    private readonly ICountriesCache _countriesCache;

    /// <summary>
    /// Countries Controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="countriesCache">REDIS cache instance.</param>
    public CountriesController(IMediator mediator, ICountriesCache countriesCache)
        : base(mediator) => _countriesCache = countriesCache;

    /// <summary>
    /// Returns list of country codes.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of country codes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<GetCountryCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetCountryCodesQueryResult>> GetCountryCodeList([FromQuery] bool noCache = false)
        => await _countriesCache.GetCountryCodes(string.Empty, noCache);

    /// <summary>
    /// Returns country code for given country.
    /// </summary>
    /// <param name="country">Country name, i.e: Austria.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Country code, i.e: 40.</returns>
    [HttpGet("{country}")]
    [ProducesResponseType(typeof(IList<GetCountryCodesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IList<GetCountryCodesQueryResult>> GetCountryCode([FromRoute] string country, [FromQuery] bool noCache = false)
        => await _countriesCache.GetCountryCodes(country, noCache);
}
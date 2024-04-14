using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Invoicing.Templates.Queries;
using TokanPages.Backend.Application.Invoicing.Templates.Queries.Models;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Templates cache implementation.
/// </summary>
public class TemplatesCache : ITemplatesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Payments cache implementation.
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="mediator">Mediator instance.</param>
    public TemplatesCache(IRedisDistributedCache redisDistributedCache, IHostEnvironment environment, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _environment = environment;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<IList<InvoiceTemplateInfo>> GetInvoiceTemplates(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetInvoiceTemplatesQuery());

        var key = $"{_environment.EnvironmentName}:templates:info";
        var value = await _redisDistributedCache.GetObjectAsync<IList<InvoiceTemplateInfo>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetInvoiceTemplatesQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}
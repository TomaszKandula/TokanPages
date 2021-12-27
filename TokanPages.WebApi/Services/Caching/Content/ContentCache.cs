namespace TokanPages.WebApi.Services.Caching.Content
{
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Backend.Cqrs.Handlers.Queries.Content;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public class ContentCache : IContentCache
    {
        private readonly IRedisDistributedCache _redisDistributedCache;

        private readonly IMediator _mediator;

        public ContentCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
        {
            _redisDistributedCache = redisDistributedCache;
            _mediator = mediator;
        }

        public async Task<GetContentQueryResult> GetContent(string type, string name, string language, bool noCache = false)
        {
            var key = $"{type}/{name}/{language}";
            if (noCache)
                return await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });

            var value = await _redisDistributedCache.GetObjectAsync<GetContentQueryResult>(key);
            if (value is not null) return value;

            value = await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });
            await _redisDistributedCache.SetObjectAsync(key, value);

            return value;
        }
    }
}
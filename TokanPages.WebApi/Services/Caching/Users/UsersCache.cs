namespace TokanPages.WebApi.Services.Caching.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
    using MediatR;

    public class UsersCache : IUsersCache
    {
        private readonly IRedisDistributedCache _redisDistributedCache;

        private readonly IMediator _mediator;

        public UsersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
        {
            _redisDistributedCache = redisDistributedCache;
            _mediator = mediator;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public async Task<IEnumerable<GetAllUsersQueryResult>> GetUsers(bool noCache = false)
        {
            const string key = "GetAllUsersQueryResult";
            if (noCache)
                return await _mediator.Send(new GetAllUsersQuery());

            var value = await _redisDistributedCache.GetObjectAsync<IEnumerable<GetAllUsersQueryResult>>(key);
            if (value is not null && value.Any()) return value;

            value = await _mediator.Send(new GetAllUsersQuery());
            await _redisDistributedCache.SetObjectAsync(key, value);

            return value;
        }

        public async Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false)
        {
            var key = $"user-{id:N}";
            if (noCache)
                return await _mediator.Send(new GetUserQuery { Id = id });

            var value = await _redisDistributedCache.GetObjectAsync<GetUserQueryResult>(key);
            if (value is not null) return value;

            value = await _mediator.Send(new GetUserQuery { Id = id });
            await _redisDistributedCache.SetObjectAsync(key, value);

            return value;
        }
    }
}
namespace TokanPages.WebApi.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Attributes;
    using Backend.Cqrs.Mappers;
    using Backend.Identity.Authorization;
    using Backend.Shared.Dto.Subscribers;
    using Backend.Cqrs.Handlers.Queries.Subscribers;
    using Services.Caching.Subscribers;
    using MediatR;

    [Authorize]
    public class SubscribersController : ApiBaseController
    {
        private readonly ISubscribersCache _subscribersCache;

        public SubscribersController(IMediator mediator, ISubscribersCache subscribersCache) 
            : base(mediator) => _subscribersCache = subscribersCache;

        [HttpGet]
        [AuthorizeUser(Roles.GodOfAsgard)]
        public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers([FromQuery] bool noCache = false)
            => await _subscribersCache.GetSubscribers(noCache);

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid id, [FromQuery] bool noCache = false)
            => await _subscribersCache.GetSubscriber(id, noCache);

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto payLoad) 
            => await Mediator.Send(SubscribersMapper.MapToAddSubscriberCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto payLoad)
            => await Mediator.Send(SubscribersMapper.MapToUpdateSubscriberCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto payLoad)
         => await Mediator.Send(SubscribersMapper.MapToRemoveSubscriberCommand(payLoad));
    }
}
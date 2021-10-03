namespace TokanPages.WebApi.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Mappers;
    using Backend.Identity.Attributes;
    using Backend.Identity.Authorization;
    using Backend.Shared.Dto.Subscribers;
    using Backend.Cqrs.Handlers.Queries.Subscribers;
    using MediatR;

    [Authorize]
    public class SubscribersController : ApiBaseController
    {
        public SubscribersController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers()
            => await FMediator.Send(new GetAllSubscribersQuery());

        [HttpGet("{AId}")]
        [AllowAnonymous]
        public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid AId)
            => await FMediator.Send(new GetSubscriberQuery { Id = AId });

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto APayLoad) 
            => await FMediator.Send(SubscribersMapper.MapToAddSubscriberCommand(APayLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto APayLoad)
            => await FMediator.Send(SubscribersMapper.MapToUpdateSubscriberCommand(APayLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto APayLoad)
         => await FMediator.Send(SubscribersMapper.MapToRemoveSubscriberCommand(APayLoad));
    }
}
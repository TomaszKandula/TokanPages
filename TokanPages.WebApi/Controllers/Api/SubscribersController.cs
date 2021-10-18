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
        public SubscribersController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers()
            => await Mediator.Send(new GetAllSubscribersQuery());

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid id)
            => await Mediator.Send(new GetSubscriberQuery { Id = id });

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
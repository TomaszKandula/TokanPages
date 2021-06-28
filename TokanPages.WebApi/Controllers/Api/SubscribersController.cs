using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Subscribers;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;
using MediatR;

namespace TokanPages.WebApi.Controllers.Api
{
    public class SubscribersController : BaseController
    {
        public SubscribersController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers()
            => await FMediator.Send(new GetAllSubscribersQuery());

        [HttpGet("{AId}")]
        public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid AId)
            => await FMediator.Send(new GetSubscriberQuery { Id = AId });

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto APayLoad) 
            => await FMediator.Send(SubscribersMapper.MapToAddSubscriberCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto APayLoad)
            => await FMediator.Send(SubscribersMapper.MapToUpdateSubscriberCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto APayLoad)
         => await FMediator.Send(SubscribersMapper.MapToRemoveSubscriberCommand(APayLoad));
    }
}

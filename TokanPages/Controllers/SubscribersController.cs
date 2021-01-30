using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Subscribers;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;
using MediatR;

namespace TokanPages.Controllers
{
    public class SubscribersController : __BaseController
    {
        public SubscribersController(IMediator AMediator) : base(AMediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers()
        {
            var LQuery = new GetAllSubscribersQuery();
            return await FMediator.Send(LQuery);
        }

        [HttpGet("{Id}")]
        public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid Id)
        {
            var LQuery = new GetSubscriberQuery { Id = Id };
            return await FMediator.Send(LQuery);
        }

        [HttpPost]
        public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto APayLoad) 
        {
            var LCommand = SubscribersMapper.MapToAddSubscriberCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto APayLoad)
        {
            var LCommand = SubscribersMapper.MapToUpdateSubscriberCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto APayLoad)
        {
            var LCommand = SubscribersMapper.MapToRemoveSubscriberCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }
    }
}

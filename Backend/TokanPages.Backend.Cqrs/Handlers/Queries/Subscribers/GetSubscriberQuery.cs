using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetSubscriberQuery : IRequest<GetSubscriberQueryResult>
    {
        public Guid Id { get; set; }
    }
}

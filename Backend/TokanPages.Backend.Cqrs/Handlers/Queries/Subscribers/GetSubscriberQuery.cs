using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetSubscriberQuery : IRequest<Domain.Entities.Subscribers>
    {
        public Guid Id { get; set; }
    }
}

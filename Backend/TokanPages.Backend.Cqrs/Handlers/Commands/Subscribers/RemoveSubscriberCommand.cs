using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class RemoveSubscriberCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

}

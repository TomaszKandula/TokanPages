using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    public class AddSubscriberCommand : IRequest<Guid>
    {
        public string Email { get; set; }
    }
}

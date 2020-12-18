using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class UpdateSubscriberCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsActivated { get; set; }
        public int Count { get; set; }
    }

}

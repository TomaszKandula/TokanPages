using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class AddSubscriberCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }

}

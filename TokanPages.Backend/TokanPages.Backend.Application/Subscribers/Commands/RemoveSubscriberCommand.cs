using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class RemoveSubscriberCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class RemoveNewsletterCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
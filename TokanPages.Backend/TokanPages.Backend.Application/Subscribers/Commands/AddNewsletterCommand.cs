using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class AddNewsletterCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}
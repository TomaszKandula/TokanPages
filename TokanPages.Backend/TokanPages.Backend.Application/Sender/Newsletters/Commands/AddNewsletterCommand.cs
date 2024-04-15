using MediatR;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class AddNewsletterCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}
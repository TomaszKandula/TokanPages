using MediatR;

namespace TokanPages.Backend.Application.Newsletters.Commands;

public class AddNewsletterCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}
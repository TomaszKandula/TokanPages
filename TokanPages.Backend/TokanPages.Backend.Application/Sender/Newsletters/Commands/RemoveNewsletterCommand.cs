using MediatR;

namespace TokanPages.Backend.Application.Newsletters.Commands;

public class RemoveNewsletterCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
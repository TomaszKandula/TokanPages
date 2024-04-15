using MediatR;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class UpdateNewsletterCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public string? Email { get; set; }
        
    public bool? IsActivated { get; set; }
        
    public int? Count { get; set; }
}
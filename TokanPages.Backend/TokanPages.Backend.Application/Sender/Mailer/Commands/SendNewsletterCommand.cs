using MediatR;
using TokanPages.Backend.Application.Mailer.Models;

namespace TokanPages.Backend.Application.Mailer.Commands;

public class SendNewsletterCommand : IRequest<Unit>
{
    public IEnumerable<SubscriberInfo>? SubscriberInfo { get; set; }
        
    public string Subject { get; set; } = "";
        
    public string Message { get; set; } = "";
}
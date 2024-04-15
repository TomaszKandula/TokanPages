using MediatR;
using TokanPages.Backend.Application.Sender.Mailer.Models;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendNewsletterCommand : IRequest<Unit>
{
    public IEnumerable<SubscriberInfo>? SubscriberInfo { get; set; }
        
    public string Subject { get; set; } = "";
        
    public string Message { get; set; } = "";
}
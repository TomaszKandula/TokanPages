using MediatR;
using TokanPages.Backend.Application.Sender.Mailer.Models;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendNewsletterCommand : IRequest<Unit>
{
    public string LanguageId { get; set; } = "en";

    public IEnumerable<SubscriberInfo>? SubscriberInfo { get; set; }
        
    public string Subject { get; set; } = "";
        
    public string Message { get; set; } = "";
}
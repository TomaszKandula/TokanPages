namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

using System.Collections.Generic;
using Shared.Models;
using MediatR;

public class SendNewsletterCommand : IRequest<Unit>
{
    public List<SubscriberInfo> SubscriberInfo { get; set; }
        
    public string Subject { get; set; }
        
    public string Message { get; set; }
}
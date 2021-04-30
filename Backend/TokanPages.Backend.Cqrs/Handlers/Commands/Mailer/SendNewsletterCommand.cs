using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendNewsletterCommand : IRequest<Unit>
    {
        public List<SubscriberInfo> SubscriberInfo { get; set; }
        
        public string Subject { get; set; }
        
        public string Message { get; set; }
    }
}

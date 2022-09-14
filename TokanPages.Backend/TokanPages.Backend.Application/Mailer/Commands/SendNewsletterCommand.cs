using MediatR;
using TokanPages.WebApi.Dto.Mailer.Models;

namespace TokanPages.Backend.Application.Mailer.Commands;

public class SendNewsletterCommand : IRequest<Unit>
{
    public List<SubscriberInfo> SubscriberInfo { get; set; } = new();
        
    public string Subject { get; set; } = "";
        
    public string Message { get; set; } = "";
}
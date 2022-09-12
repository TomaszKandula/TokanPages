namespace TokanPages.Backend.Application.Mailer.Commands;

using System.Collections.Generic;
using WebApi.Dto.Mailer.Models;
using MediatR;

public class SendNewsletterCommand : IRequest<Unit>
{
    public List<SubscriberInfo> SubscriberInfo { get; set; } = new();
        
    public string Subject { get; set; } = "";
        
    public string Message { get; set; } = "";
}
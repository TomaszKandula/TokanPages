using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Handlers.Commands.Mailer;
using TokanPages.WebApi.Dto.Mailer;

namespace TokanPages.WebApi.Controllers.Mappers;

[ExcludeFromCodeCoverage]
public static class MailerMapper
{
    public static SendMessageCommand MapToSendMessageCommand(SendMessageDto model) => new()
    {
        UserEmail = model.UserEmail,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailFrom = model.EmailFrom,
        EmailTos = model.EmailTos,
        Subject = model.Subject,
        Message = model.Message
    };

    public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterDto model) => new() 
    { 
        SubscriberInfo = model.SubscriberInfo,
        Subject = model.Subject,
        Message = model.Message
    };
}
namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Handlers.Commands.Mailer;
    using Shared.Dto.Mailer;

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
}
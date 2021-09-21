namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Handlers.Commands.Mailer;
    using Shared.Dto.Mailer;

    [ExcludeFromCodeCoverage]
    public static class MailerMapper
    {
        public static VerifyEmailAddressCommand MapToVerifyEmailAddressCommand(VerifyEmailAddressDto AModel) => new() 
        { 
            Email = AModel.Email
        };

        public static SendMessageCommand MapToSendMessageCommand(SendMessageDto AModel) => new()
        {
            UserEmail = AModel.UserEmail,
            FirstName = AModel.FirstName,
            LastName = AModel.LastName,
            EmailFrom = AModel.EmailFrom,
            EmailTos = AModel.EmailTos,
            Subject = AModel.Subject,
            Message = AModel.Message
        };

        public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterDto AModel) => new() 
        { 
            SubscriberInfo = AModel.SubscriberInfo,
            Subject = AModel.Subject,
            Message = AModel.Message
        };
    }
}
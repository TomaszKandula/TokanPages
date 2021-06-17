using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Shared.Dto.Mailer;

namespace TokanPages.Backend.Cqrs.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class MailerMapper
    {
        public static VerifyEmailAddressCommand MapToVerifyEmailAddressCommand(VerifyEmailAddressDto AModel) 
        {
            return new () 
            { 
                Email = AModel.Email
            };
        }

        public static SendMessageCommand MapToSendMessageCommand(SendMessageDto AModel) 
        {
            return new ()
            {
                UserEmail = AModel.UserEmail,
                FirstName = AModel.FirstName,
                LastName = AModel.LastName,
                EmailFrom = AModel.EmailFrom,
                EmailTos = AModel.EmailTos,
                Subject = AModel.Subject,
                Message = AModel.Message
            };
        }

        public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterDto AModel) 
        {
            return new () 
            { 
                SubscriberInfo = AModel.SubscriberInfo,
                Subject = AModel.Subject,
                Message = AModel.Message
            };
        }
    }
}

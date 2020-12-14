using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Shared.Dto.Mailer;

namespace TokanPages.Backend.Cqrs.Mappers
{

    public static class MailerMapper
    {

        public static VerifyEmailAddressCommand MapToVerifyEmailAddressCommand(VerifyEmailAddressRequest AModel) 
        {
            return new VerifyEmailAddressCommand 
            { 
                Email = AModel.Email
            };
        }

        public static SendMessageCommand MapToSendMessageCommand(SendMessageRequest AModel) 
        {
            return new SendMessageCommand
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

        public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterRequest AModel) 
        {
            return new SendNewsletterCommand 
            { 
                SubscriberInfo = AModel.SubscriberInfo,
                Subject = AModel.Subject,
                Message = AModel.Message
            };
        }

    }

}

using TokanPages.Backend.Cqrs.Handlers.Commands;

namespace TokanPages.Backend.Cqrs.Mappers
{

    public static class MailerMapper
    {

        public static VerifyEmailAddressCommand MapToVerifyEmailAddressCommand(TokanPages.Backend.Shared.Dto.Requests.VerifyEmailAddressRequest AModel) 
        {
            return new VerifyEmailAddressCommand 
            { 
                Email = AModel.Email
            };
        }

    }

}

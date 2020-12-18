using TokanPages.Backend.Shared.Dto.Mailer;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class VerifyEmailAddressCommand : IRequest<VerifyEmailAddressCommandResult>
    {
        public string Email { get; set; }
    }

}

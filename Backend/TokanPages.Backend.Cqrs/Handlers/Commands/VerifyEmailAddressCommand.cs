using TokanPages.Backend.Shared.Dto.Responses;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands
{

    public class VerifyEmailAddressCommand : IRequest<VerifyEmailAddressDto>
    {
        public string Email { get; set; }
    }

}

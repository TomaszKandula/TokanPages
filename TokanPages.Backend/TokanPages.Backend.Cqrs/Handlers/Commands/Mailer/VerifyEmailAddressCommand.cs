namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using MediatR;

    public class VerifyEmailAddressCommand : IRequest<VerifyEmailAddressCommandResult>
    {
        public string Email { get; set; }
    }
}
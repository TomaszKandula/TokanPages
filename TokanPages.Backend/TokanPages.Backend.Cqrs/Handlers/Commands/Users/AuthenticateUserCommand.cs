namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using MediatR;

    public class AuthenticateUserCommand : IRequest<AuthenticateUserCommandResult>
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }        
    }
}
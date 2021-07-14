namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using Shared.Dto.Users;

    public class AuthenticateUserCommandResult : GetUserDto
    {
        public string Jwt { get; set; }
    }
}
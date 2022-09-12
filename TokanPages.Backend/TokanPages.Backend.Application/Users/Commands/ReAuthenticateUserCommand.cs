namespace TokanPages.Backend.Application.Handlers.Commands.Users;

using MediatR;

public class ReAuthenticateUserCommand : IRequest<ReAuthenticateUserCommandResult>
{
    public string? RefreshToken { get; set; }
}

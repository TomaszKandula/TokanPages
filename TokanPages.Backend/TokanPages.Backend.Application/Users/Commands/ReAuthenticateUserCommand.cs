namespace TokanPages.Backend.Application.Users.Commands;

using MediatR;

public class ReAuthenticateUserCommand : IRequest<ReAuthenticateUserCommandResult>
{
    public string? RefreshToken { get; set; }
}

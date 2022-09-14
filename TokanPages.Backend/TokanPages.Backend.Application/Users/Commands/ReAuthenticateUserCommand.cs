using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class ReAuthenticateUserCommand : IRequest<ReAuthenticateUserCommandResult>
{
    public string? RefreshToken { get; set; }
}

using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class ReAuthenticateUserCommand : IRequest<ReAuthenticateUserCommandResult>
{
    public Guid UserId { get; set; }

    public string? RefreshToken { get; set; }
}

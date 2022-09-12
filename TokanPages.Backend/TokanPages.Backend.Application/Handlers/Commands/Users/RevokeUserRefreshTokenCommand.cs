namespace TokanPages.Backend.Application.Handlers.Commands.Users;

using MediatR;

public class RevokeUserRefreshTokenCommand : IRequest<Unit>
{
    public string? RefreshToken { get; set; }
}
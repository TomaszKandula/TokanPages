namespace TokanPages.Backend.Application.Users.Commands;

using MediatR;

public class RevokeUserRefreshTokenCommand : IRequest<Unit>
{
    public string? RefreshToken { get; set; }
}
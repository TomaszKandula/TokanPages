using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommand : IRequest<Unit>
{
    public string? RefreshToken { get; set; }
}
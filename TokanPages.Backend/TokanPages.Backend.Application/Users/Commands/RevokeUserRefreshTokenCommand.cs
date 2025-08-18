using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommand : IRequest<Unit> { }

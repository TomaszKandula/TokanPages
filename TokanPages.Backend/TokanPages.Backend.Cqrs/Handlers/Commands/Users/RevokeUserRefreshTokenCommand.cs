namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using MediatR;

    public class RevokeUserRefreshTokenCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; }
    }
}
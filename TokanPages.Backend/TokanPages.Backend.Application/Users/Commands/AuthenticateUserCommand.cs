using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommand : IRequest<AuthenticateUserCommandResult>
{
    public string? EmailAddress { get; set; }

    public string? Password { get; set; }        
}
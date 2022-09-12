namespace TokanPages.Backend.Application.Users.Commands;

using MediatR;

public class AuthenticateUserCommand : IRequest<AuthenticateUserCommandResult>
{
    public string? EmailAddress { get; set; }

    public string? Password { get; set; }        
}
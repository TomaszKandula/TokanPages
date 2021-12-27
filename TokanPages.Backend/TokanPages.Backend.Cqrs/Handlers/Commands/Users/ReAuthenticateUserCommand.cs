namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class ReAuthenticateUserCommand : IRequest<ReAuthenticateUserCommandResult>
{
    public Guid Id { get; set; } 
}
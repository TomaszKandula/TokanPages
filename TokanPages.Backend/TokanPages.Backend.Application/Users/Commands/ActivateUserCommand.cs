namespace TokanPages.Backend.Application.Handlers.Commands.Users;

using System;
using MediatR;

public class ActivateUserCommand : IRequest<Unit>
{
    public Guid ActivationId { get; set; }
}
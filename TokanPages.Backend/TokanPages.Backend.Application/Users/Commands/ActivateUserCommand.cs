namespace TokanPages.Backend.Application.Users.Commands;

using System;
using MediatR;

public class ActivateUserCommand : IRequest<Unit>
{
    public Guid ActivationId { get; set; }
}
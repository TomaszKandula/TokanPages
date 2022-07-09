namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class RemoveUserCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }

    public bool IsSoftDelete { get; set; }
}
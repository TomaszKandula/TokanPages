namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class UpdateUserPasswordCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }

    public Guid? ResetId { get; set; }

    public string? OldPassword { get; set; } 

    public string? NewPassword { get; set; }
}
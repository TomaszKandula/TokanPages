using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserPasswordCommand : IRequest<Unit>
{
    public Guid? Id { get; init; }

    public Guid? ResetId { get; init; }

    public string? OldPassword { get; init; } 

    public required string NewPassword { get; init; }
}
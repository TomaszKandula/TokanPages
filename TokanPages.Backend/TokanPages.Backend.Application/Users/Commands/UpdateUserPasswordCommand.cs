using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserPasswordCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }

    public Guid? ResetId { get; set; }

    public string? OldPassword { get; set; } 

    public string? NewPassword { get; set; }
}
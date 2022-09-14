using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }

    public bool IsSoftDelete { get; set; }
}
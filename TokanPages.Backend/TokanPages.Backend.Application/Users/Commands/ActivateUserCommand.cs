using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommand : IRequest<ActivateUserCommandResult>
{
    public Guid ActivationId { get; set; }
}
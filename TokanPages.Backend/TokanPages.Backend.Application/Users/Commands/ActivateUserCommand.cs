using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommand : IRequest<Unit>
{
    public Guid ActivationId { get; set; }
}
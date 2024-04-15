using MediatR;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class RemoveSubscriptionCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }
}
using MediatR;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class RemoveSubscriptionCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }
}
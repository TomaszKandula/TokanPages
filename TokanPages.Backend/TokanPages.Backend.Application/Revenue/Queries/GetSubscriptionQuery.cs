using MediatR;

namespace TokanPages.Backend.Application.Subscriptions.Queries;

public class GetSubscriptionQuery : IRequest<GetSubscriptionQueryResult>
{
    public Guid? UserId { get; set; }
}
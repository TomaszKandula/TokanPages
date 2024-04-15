using MediatR;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionQuery : IRequest<GetSubscriptionQueryResult>
{
    public Guid? UserId { get; set; }
}
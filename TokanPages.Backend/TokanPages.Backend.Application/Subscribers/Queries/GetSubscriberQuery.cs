using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetSubscriberQuery : IRequest<GetSubscriberQueryResult>
{
    public Guid Id { get; set; }
}
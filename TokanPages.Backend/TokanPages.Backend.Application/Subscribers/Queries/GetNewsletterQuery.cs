using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetNewsletterQuery : IRequest<GetNewsletterQueryResult>
{
    public Guid Id { get; set; }
}
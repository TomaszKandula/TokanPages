using MediatR;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewsletterQuery : IRequest<GetNewsletterQueryResult>
{
    public Guid Id { get; set; }
}
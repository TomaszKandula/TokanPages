using MediatR;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewslettersQuery : IRequest<List<GetNewslettersQueryResult>> { }
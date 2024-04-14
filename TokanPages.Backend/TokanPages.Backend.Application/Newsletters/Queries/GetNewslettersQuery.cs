using MediatR;

namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetNewslettersQuery : IRequest<List<GetNewslettersQueryResult>> { }
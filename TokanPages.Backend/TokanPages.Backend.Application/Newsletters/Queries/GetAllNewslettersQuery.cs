using MediatR;

namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetAllNewslettersQuery : IRequest<List<GetAllNewslettersQueryResult>> { }
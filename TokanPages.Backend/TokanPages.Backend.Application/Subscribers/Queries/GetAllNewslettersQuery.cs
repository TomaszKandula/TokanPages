using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetAllNewslettersQuery : IRequest<List<GetAllNewslettersQueryResult>> { }
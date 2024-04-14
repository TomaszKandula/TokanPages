using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUsersQuery : IRequest<List<GetUsersQueryResult>> { }
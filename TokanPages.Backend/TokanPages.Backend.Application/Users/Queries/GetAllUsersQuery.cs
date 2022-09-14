using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetAllUsersQuery : IRequest<List<GetAllUsersQueryResult>> { }
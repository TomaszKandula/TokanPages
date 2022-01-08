namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users;

using System.Collections.Generic;
using MediatR;

public class GetAllUsersQuery : IRequest<List<GetAllUsersQueryResult>> { }
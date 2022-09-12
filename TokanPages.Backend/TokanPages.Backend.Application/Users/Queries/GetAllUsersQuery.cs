namespace TokanPages.Backend.Application.Users.Queries;

using System.Collections.Generic;
using MediatR;

public class GetAllUsersQuery : IRequest<List<GetAllUsersQueryResult>> { }
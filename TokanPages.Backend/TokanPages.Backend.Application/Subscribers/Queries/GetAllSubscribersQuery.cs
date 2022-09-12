namespace TokanPages.Backend.Application.Handlers.Queries.Subscribers;

using System.Collections.Generic;
using MediatR;

public class GetAllSubscribersQuery : IRequest<List<GetAllSubscribersQueryResult>> { }
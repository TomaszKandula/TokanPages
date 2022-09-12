namespace TokanPages.Backend.Application.Subscribers.Queries;

using System.Collections.Generic;
using MediatR;

public class GetAllSubscribersQuery : IRequest<List<GetAllSubscribersQueryResult>> { }
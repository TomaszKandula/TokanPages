using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetAllSubscribersQuery : IRequest<List<GetAllSubscribersQueryResult>> { }
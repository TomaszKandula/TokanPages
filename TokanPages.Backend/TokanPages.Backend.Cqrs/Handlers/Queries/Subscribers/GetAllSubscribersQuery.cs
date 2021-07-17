namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllSubscribersQuery : IRequest<IEnumerable<GetAllSubscribersQueryResult>> { }
}
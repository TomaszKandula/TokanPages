using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{

    public class GetAllSubscribersQuery : IRequest<IEnumerable<Domain.Entities.Subscribers>>
    {
    }

}

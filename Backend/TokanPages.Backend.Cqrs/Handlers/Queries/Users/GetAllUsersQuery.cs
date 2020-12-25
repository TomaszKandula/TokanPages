using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    
    public class GetAllUsersQuery : IRequest<IEnumerable<Domain.Entities.Users>>
    {
    }

}

using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetAllArticlesCommand : IRequest<IEnumerable<Domain.Entities.Articles>>
    {
    }

}

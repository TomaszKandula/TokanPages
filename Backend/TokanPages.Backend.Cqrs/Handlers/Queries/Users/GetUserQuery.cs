using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetUserQuery : IRequest<GetUserQueryResult>
    {
        public Guid Id { get; set; }
    }
}

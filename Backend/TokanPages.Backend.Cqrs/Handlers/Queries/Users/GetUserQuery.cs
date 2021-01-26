using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetUserQuery : IRequest<Domain.Entities.Users>
    {
        public Guid Id { get; set; }
    }
}

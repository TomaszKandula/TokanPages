using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{  
    public class RemoveUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}

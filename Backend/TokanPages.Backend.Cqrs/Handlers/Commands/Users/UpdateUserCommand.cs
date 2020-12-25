using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{

    public class UpdateUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserAlias { get; set; }
        public bool IsActivated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }

}

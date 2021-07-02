using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{   
    public class AddUserCommand : IRequest<Guid>
    {
        public string UserAlias { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string CryptedPassword { get; set; }
    }
}

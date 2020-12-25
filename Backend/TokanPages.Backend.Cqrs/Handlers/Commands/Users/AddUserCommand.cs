using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    
    public class AddUserCommand : IRequest<Unit>
    {
        public string UserAlias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }

}

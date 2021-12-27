namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class AddUserCommand : IRequest<Guid>
{
    public string UserAlias { get; set; }

    public string FirstName { get; set; }
        
    public string LastName { get; set; }
        
    public string EmailAddress { get; set; }
        
    public string Password { get; set; }
}
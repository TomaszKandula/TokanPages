using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserCommand : IRequest<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? Password { get; set; }
}
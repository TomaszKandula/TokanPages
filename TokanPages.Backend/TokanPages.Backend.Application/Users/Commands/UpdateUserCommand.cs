using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserCommand : IRequest<UpdateUserCommandResult>
{
    public Guid? Id { get; set; }

    public bool? IsActivated { get; set; }

    public string? UserAlias { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? UserAboutText { get; set; }

    public string? UserImageName { get; set; }

    public string? UserVideoName { get; set; } 
}
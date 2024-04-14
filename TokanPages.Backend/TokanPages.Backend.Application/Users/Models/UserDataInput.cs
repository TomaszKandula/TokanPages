using TokanPages.Backend.Application.Users.Commands;

namespace TokanPages.Backend.Application.Users.Models;

public class UserDataInput
{
    public Guid UserId { get; set; }

    public AddUserCommand Command { get; set; } = new();
}
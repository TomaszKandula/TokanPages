namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class GetUserDto
{
    public Guid UserId { get; set; }

    public string AliasName { get; set; }

    public string AvatarName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string ShortBio { get; set; }

    public DateTime Registered { get; set; }
}
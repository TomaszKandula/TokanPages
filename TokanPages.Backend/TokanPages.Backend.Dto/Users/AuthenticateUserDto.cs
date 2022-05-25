namespace TokanPages.Backend.Dto.Users;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AuthenticateUserDto
{
    public string EmailAddress { get; set; }

    public string Password { get; set; }        
}
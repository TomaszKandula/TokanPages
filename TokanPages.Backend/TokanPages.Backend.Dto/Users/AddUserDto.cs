namespace TokanPages.Backend.Dto.Users;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to register user account
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Password { get; set; }
}
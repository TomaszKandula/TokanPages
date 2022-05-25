#nullable enable
namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    public Guid? Id { get; set; }

    public bool IsActivated { get; set; }

    public string? UserAlias { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? ShortBio { get; set; }
}
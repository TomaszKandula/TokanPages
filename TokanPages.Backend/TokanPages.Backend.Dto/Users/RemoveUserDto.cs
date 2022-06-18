namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to remove existing user
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserDto
{
    /// <summary>
    /// Optional
    /// </summary>
    public Guid? Id { get; set; }
}
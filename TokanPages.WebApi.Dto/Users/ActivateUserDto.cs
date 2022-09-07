namespace TokanPages.WebApi.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to activate user account
/// </summary>
[ExcludeFromCodeCoverage]
public class ActivateUserDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid ActivationId { get; set; }
}
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

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
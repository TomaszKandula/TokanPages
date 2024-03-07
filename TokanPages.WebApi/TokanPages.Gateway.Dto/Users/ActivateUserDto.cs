using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Gateway.Dto.Users;

/// <summary>
/// Use it when you want to activate user account.
/// </summary>
[ExcludeFromCodeCoverage]
public class ActivateUserDto
{
    /// <summary>
    /// Activation identification.
    /// </summary>
    public Guid ActivationId { get; set; }
}
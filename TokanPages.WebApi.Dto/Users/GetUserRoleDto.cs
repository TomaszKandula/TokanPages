using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it to get user role
/// </summary>
[ExcludeFromCodeCoverage]
public class GetUserRoleDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = "";
}
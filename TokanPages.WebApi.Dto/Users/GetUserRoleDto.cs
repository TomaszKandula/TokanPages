namespace TokanPages.WebApi.Dto.Users;

using System.Diagnostics.CodeAnalysis;

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
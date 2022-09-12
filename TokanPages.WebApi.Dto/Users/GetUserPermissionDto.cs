using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it to get user permission
/// </summary>
[ExcludeFromCodeCoverage]
public class GetUserPermissionDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = "";
}
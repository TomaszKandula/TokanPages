namespace TokanPages.WebApi.Dto.Users;

using System.Diagnostics.CodeAnalysis;

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
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

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
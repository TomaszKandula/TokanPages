namespace TokanPages.WebApi.Dto.Users;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to reset existing password
/// </summary>
[ExcludeFromCodeCoverage]
public class ResetUserPasswordDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? EmailAddress { get; set; }     
}
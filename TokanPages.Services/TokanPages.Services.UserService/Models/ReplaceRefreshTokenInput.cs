using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class ReplaceRefreshTokenInput
{
    public Guid UserId { get; set; }

    public UserRefreshTokens? SavedUserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public bool SaveImmediately { get; set; }
}
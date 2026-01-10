using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class RevokeRefreshTokensInput
{
    public IEnumerable<UserRefreshToken>? UserRefreshTokens { get; set; }

    public UserRefreshToken? SavedUserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Reason { get; set; }

    public bool SaveImmediately { get; set; }
}
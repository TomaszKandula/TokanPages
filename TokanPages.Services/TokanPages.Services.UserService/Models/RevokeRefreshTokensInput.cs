using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class RevokeRefreshTokensInput
{
    public IEnumerable<UserRefreshTokens>? UserRefreshTokens { get; set; }

    public UserRefreshTokens? SavedUserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Reason { get; set; }

    public bool SaveImmediately { get; set; }
}
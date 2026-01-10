using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class RevokeRefreshTokenInput
{
    public UserRefreshToken? UserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Reason { get; set; }

    public string? ReplacedByToken  { get; set; } 

    public bool SaveImmediately  { get; set; }
}
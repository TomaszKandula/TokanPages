using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class RevokeRefreshTokenInput
{
    public UserRefreshTokens? UserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Reason { get; set; }

    public string? ReplacedByToken  { get; set; } 

    public bool SaveImmediately  { get; set; }
}
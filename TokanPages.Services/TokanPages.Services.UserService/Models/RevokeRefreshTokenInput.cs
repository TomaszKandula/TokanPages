namespace TokanPages.Services.UserService.Models;

using Backend.Domain.Entities;

public class RevokeRefreshTokenInput
{
    public UserRefreshTokens? UserRefreshTokens { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Reason { get; set; }

    public string? ReplacedByToken  { get; set; } 

    public bool SaveImmediately  { get; set; }
}
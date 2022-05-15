namespace TokanPages.Services.UserService.Models;

using Backend.Domain.Entities;

public class ReplaceRefreshTokenInput
{
    public Guid UserId { get; set; }

    public UserRefreshTokens SavedUserRefreshTokens { get; set; }

    public string RequesterIpAddress { get; set; }

    public bool SaveImmediately { get; set; }
}
namespace TokanPages.Chat.Dto.Chat;

/// <summary>
/// Use it when you want to remove chat cache.
/// </summary>
public class RemoveChatCacheDto
{
    /// <summary>
    /// Unique chat key.
    /// </summary>
    public string? ChatKey { get; set; }

    /// <summary>
    /// Unique chat ID.
    /// </summary>
    public Guid? ChatId { get; set; }
}
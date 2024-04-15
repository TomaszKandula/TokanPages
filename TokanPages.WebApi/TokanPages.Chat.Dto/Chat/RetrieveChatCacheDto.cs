namespace TokanPages.Chat.Dto.Chat;

/// <summary>
/// Use it when you want to retrieve chat cache.
/// </summary>
public class RetrieveChatCacheDto
{
    /// <summary>
    /// Unique chat key.
    /// </summary>
    public string[] ChatKey { get; set; } = Array.Empty<string>();
}
namespace TokanPages.Chat.Dto.Chat;

/// <summary>
/// Use it when you want to post a new chat message.
/// </summary>
public class PostChatMessageDto
{
    /// <summary>
    /// Mandatory unique chat key.
    /// </summary>
    public string ChatKey { get; set; } = "";

    /// <summary>
    /// Mandatory text message. Can contain emojis and some HTML tags.
    /// </summary>
    public string Message { get; set; } = "";
}
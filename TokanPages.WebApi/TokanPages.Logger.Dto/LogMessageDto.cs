namespace TokanPages.Logger.Dto;

/// <summary>
/// Use it when you want to log an error message from the ClientApp.
/// </summary>
public class LogMessageDto
{
    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    /// Error details, stack trace from the frontend.
    /// </summary>
    public string Exception { get; set; } = "";

    /// <summary>
    /// Severity (debug, info, warning, error, fatal).
    /// </summary>
    public string Severity { get; set; } = "";
}
using TokanPages.Logger.Dto.Models;

namespace TokanPages.Logger.Dto;

/// <summary>
/// Use it when you want to log an error message from the ClientApp.
/// </summary>
public class LogMessageDto
{
    /// <summary>
    /// Event date and time, when the exception occured.
    /// </summary>
    public DateTime EventDateTime { get; set; }

    /// <summary>
    /// Type of event.
    /// </summary>
    public string EventType { get; set; } = "";

    /// <summary>
    /// Severity (debug, info, warning, error, fatal).
    /// </summary>
    public string Severity { get; set; } = "";

    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    /// Error details, stack trace from the frontend.
    /// </summary>
    public string StackTrace { get; set; } = "";

    /// <summary>
    /// Current URL of the page, when the exception occured.
    /// </summary>
    public string PageUrl { get; set; } = "";

    /// <summary>
    /// User agent string for the current browser.
    /// </summary>
    public string UserAgent { get; set; } = "";

    /// <summary>
    /// Client detailed information parsed from user agent string.
    /// </summary>
    public Parsed Parsed { get; set; } = new();
}
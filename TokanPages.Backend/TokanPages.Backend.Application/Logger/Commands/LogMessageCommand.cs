using MediatR;

namespace TokanPages.Backend.Application.Logger.Commands;

public class LogMessageCommand : IRequest<Unit>
{
    public DateTime EventDateTime { get; set; }

    public string EventType { get; set; } = "";

    public string Severity { get; set; } = "";

    public string Message { get; set; } = "";

    public string StackTrace { get; set; } = "";

    public string PageUrl { get; set; } = "";

    public string BrowserName { get; set; } = "";

    public string BrowserVersion { get; set; } = "";

    public string UserAgent { get; set; } = "";
}
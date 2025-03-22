using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Logger.Commands;
using TokanPages.Logger.Dto;

namespace TokanPages.Logger.Controllers.Mappers;

/// <summary>
/// Logger mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class LoggerMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static LogMessageCommand MapToLogMessageCommand(LogMessageDto model) => new ()
    {
        EventDateTime = model.EventDateTime,
        EventType = model.EventType,
        Severity = model.Severity,
        Message = model.Message,
        StackTrace = model.StackTrace,
        PageUrl = model.PageUrl,
        BrowserName = model.BrowserName,
        BrowserVersion = model.BrowserVersion,
        UserAgent = model.UserAgent
    };
}
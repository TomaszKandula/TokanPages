using System.Text;
using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Logger.Commands;

public class LogMessageCommandHandler :  RequestHandler<LogMessageCommand, Unit>
{
    public LogMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(LogMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new StringBuilder();
        message.AppendLine($"[ClientApp event date]: {request.EventDateTime:yyyy-MM-dd HH:mm:ss}");
        message.AppendLine($"[ClientApp event type]: {request.EventType}");
        message.AppendLine($"[ClientApp page url]: {request.PageUrl}");
        message.AppendLine($"[Message]: {request.Message}");
        message.AppendLine($"[StackTrace]: {request.StackTrace}");
        message.AppendLine($"[Browser]: {request.BrowserName} {request.BrowserVersion}");
        message.AppendLine($"[UserAgent]: {request.UserAgent}");

        LogMessage(message.ToString(), request.Severity);
        return await Task.FromResult(Unit.Value);
    }

    private void LogMessage(string message, string severity)
    {
        switch (severity)
        {
            case "debug": LoggerService.LogDebug(message); break;
            case "info": LoggerService.LogInformation(message); break;
            case "warning": LoggerService.LogWarning(message); break;
            case "error": LoggerService.LogError(message); break;
            case "fatal": LoggerService.LogFatal(message); break;
            default: throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
        }
    }
}
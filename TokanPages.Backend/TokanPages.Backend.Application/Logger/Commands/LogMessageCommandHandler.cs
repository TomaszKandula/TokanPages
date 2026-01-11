using System.Text;
using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Logger.Commands;

public class LogMessageCommandHandler :  RequestHandler<LogMessageCommand, Unit>
{
    public LogMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(LogMessageCommand request, CancellationToken cancellationToken)
    {
        var browserName = request.Parsed.Browser.Name;
        var browserType = request.Parsed.Browser.Type;
        var browserVersion = request.Parsed.Browser.Version;
        var browserMajor = request.Parsed.Browser.Major;

        var deviceModel = request.Parsed.Device.Model;
        var deviceVendor = request.Parsed.Device.Vendor;
        var deviceType = request.Parsed.Device.Type;

        var engineName = request.Parsed.Engine.Name;
        var engineVersion = request.Parsed.Engine.Version;

        var osName = request.Parsed.Os.Name;
        var osVersion = request.Parsed.Os.Version;

        var message = new StringBuilder();
        message.AppendLine($"[ClientApp event date]: {request.EventDateTime:yyyy-MM-dd HH:mm:ss}");
        message.AppendLine($"[ClientApp event type]: {request.EventType}");
        message.AppendLine($"[ClientApp page url]: {request.PageUrl}");
        message.AppendLine($"[Message]: {request.Message}");
        message.AppendLine($"[StackTrace]: {request.StackTrace}");
        message.AppendLine($"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}");
        message.AppendLine($"[Device]: {deviceModel} {deviceVendor} {deviceType}");
        message.AppendLine($"[Engine]: {engineName} {engineVersion}");
        message.AppendLine($"[O/S]: {osName} {osVersion}");
        message.AppendLine($"[Reported UserAgent]: {request.UserAgent}");

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
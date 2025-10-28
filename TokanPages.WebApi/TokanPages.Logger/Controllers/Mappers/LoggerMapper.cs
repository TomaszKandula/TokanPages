using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Logger.Commands;
using TokanPages.Backend.Application.Logger.Commands.Models;
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
        UserAgent = model.UserAgent,
        Parsed = new Parsed
        {
            Browser = new Browser
            {
                Name = model.Parsed.Browser.Name,
                Version = model.Parsed.Browser.Version,
                Type = model.Parsed.Browser.Type,
                Major = model.Parsed.Browser.Major
            },
            Cpu = new Cpu
            {
                Architecture = model.Parsed.Cpu.Architecture
            },
            Device = new Device
            {
                Model = model.Parsed.Device.Model,
                Type = model.Parsed.Device.Type,
                Vendor = model.Parsed.Device.Vendor
            },
            Engine = new Engine
            {
                Name = model.Parsed.Engine.Name,
                Version = model.Parsed.Engine.Version
            },
            Os = new Os
            {
                Name = model.Parsed.Os.Name,
                Version = model.Parsed.Os.Version
            }
        }
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static UploadLogFileCommand UploadLogFileCommand(UploadLogFileDto model) => new()
    {
        CatalogName = model.CatalogName,
        Data = model.Data,
    };
}
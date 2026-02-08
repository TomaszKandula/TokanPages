using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Logger.Commands;
using TokanPages.Backend.Application.Logger.Commands.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Logger;

public class LogMessageCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenErrorSeverity_WhenLogMessageCommand_ShouldSucceed()
    {
        // Arrange
        var command = GetCommand("error");

        var browserName = command.Parsed.Browser.Name;
        var browserType = command.Parsed.Browser.Type;
        var browserVersion = command.Parsed.Browser.Version;
        var browserMajor = command.Parsed.Browser.Major;

        var deviceModel = command.Parsed.Device.Model;
        var deviceVendor = command.Parsed.Device.Vendor;
        var deviceType = command.Parsed.Device.Type;

        var engineName = command.Parsed.Engine.Name;
        var engineVersion = command.Parsed.Engine.Version;

        var osName = command.Parsed.Os.Name;
        var osVersion = command.Parsed.Os.Version;

        var expected = $"[ClientApp event date]: {command.EventDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"[ClientApp event type]: {command.EventType}\n" +
                       $"[ClientApp page url]: {command.PageUrl}\n" +
                       $"[Message]: {command.Message}\n" +
                       $"[StackTrace]: {command.StackTrace}\n" +
                       $"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}\n" +
                       $"[Device]: {deviceModel} {deviceVendor} {deviceType}\n" +
                       $"[Engine]: {engineName} {engineVersion}\n" +
                       $"[O/S]: {osName} {osVersion}\n" +
                       $"[Reported UserAgent]: {command.UserAgent}\n";

        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedLogger.Verify(service => service.LogError(expected), Times.Once());
        mockedLogger.Verify(service => service.LogDebug(expected), Times.Never());
        mockedLogger.Verify(service => service.LogInformation(expected), Times.Never());
        mockedLogger.Verify(service => service.LogWarning(expected), Times.Never());
        mockedLogger.Verify(service => service.LogFatal(expected), Times.Never());
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenDebugSeverity_WhenLogMessageCommand_ShouldSucceed()
    {
        // Arrange
        var command = GetCommand("debug");
        var browserName = command.Parsed.Browser.Name;
        var browserType = command.Parsed.Browser.Type;
        var browserVersion = command.Parsed.Browser.Version;
        var browserMajor = command.Parsed.Browser.Major;

        var deviceModel = command.Parsed.Device.Model;
        var deviceVendor = command.Parsed.Device.Vendor;
        var deviceType = command.Parsed.Device.Type;

        var engineName = command.Parsed.Engine.Name;
        var engineVersion = command.Parsed.Engine.Version;

        var osName = command.Parsed.Os.Name;
        var osVersion = command.Parsed.Os.Version;

        var expected = $"[ClientApp event date]: {command.EventDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"[ClientApp event type]: {command.EventType}\n" +
                       $"[ClientApp page url]: {command.PageUrl}\n" +
                       $"[Message]: {command.Message}\n" +
                       $"[StackTrace]: {command.StackTrace}\n" +
                       $"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}\n" +
                       $"[Device]: {deviceModel} {deviceVendor} {deviceType}\n" +
                       $"[Engine]: {engineName} {engineVersion}\n" +
                       $"[O/S]: {osName} {osVersion}\n" +
                       $"[Reported UserAgent]: {command.UserAgent}\n";

        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedLogger.Verify(service => service.LogError(expected), Times.Never());
        mockedLogger.Verify(service => service.LogDebug(expected), Times.Once());
        mockedLogger.Verify(service => service.LogInformation(expected), Times.Never());
        mockedLogger.Verify(service => service.LogWarning(expected), Times.Never());
        mockedLogger.Verify(service => service.LogFatal(expected), Times.Never());
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenInfoSeverity_WhenLogMessageCommand_ShouldSucceed()
    {
        // Arrange
        var command = GetCommand("info");
        var browserName = command.Parsed.Browser.Name;
        var browserType = command.Parsed.Browser.Type;
        var browserVersion = command.Parsed.Browser.Version;
        var browserMajor = command.Parsed.Browser.Major;

        var deviceModel = command.Parsed.Device.Model;
        var deviceVendor = command.Parsed.Device.Vendor;
        var deviceType = command.Parsed.Device.Type;

        var engineName = command.Parsed.Engine.Name;
        var engineVersion = command.Parsed.Engine.Version;

        var osName = command.Parsed.Os.Name;
        var osVersion = command.Parsed.Os.Version;

        var expected = $"[ClientApp event date]: {command.EventDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"[ClientApp event type]: {command.EventType}\n" +
                       $"[ClientApp page url]: {command.PageUrl}\n" +
                       $"[Message]: {command.Message}\n" +
                       $"[StackTrace]: {command.StackTrace}\n" +
                       $"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}\n" +
                       $"[Device]: {deviceModel} {deviceVendor} {deviceType}\n" +
                       $"[Engine]: {engineName} {engineVersion}\n" +
                       $"[O/S]: {osName} {osVersion}\n" +
                       $"[Reported UserAgent]: {command.UserAgent}\n";

        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedLogger.Verify(service => service.LogError(expected), Times.Never());
        mockedLogger.Verify(service => service.LogDebug(expected), Times.Never());
        mockedLogger.Verify(service => service.LogInformation(expected), Times.Once());
        mockedLogger.Verify(service => service.LogWarning(expected), Times.Never());
        mockedLogger.Verify(service => service.LogFatal(expected), Times.Never());
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenWarningSeverity_WhenLogMessageCommand_ShouldSucceed()
    {
        // Arrange
        var command = GetCommand("warning");
        var browserName = command.Parsed.Browser.Name;
        var browserType = command.Parsed.Browser.Type;
        var browserVersion = command.Parsed.Browser.Version;
        var browserMajor = command.Parsed.Browser.Major;

        var deviceModel = command.Parsed.Device.Model;
        var deviceVendor = command.Parsed.Device.Vendor;
        var deviceType = command.Parsed.Device.Type;

        var engineName = command.Parsed.Engine.Name;
        var engineVersion = command.Parsed.Engine.Version;

        var osName = command.Parsed.Os.Name;
        var osVersion = command.Parsed.Os.Version;

        var expected = $"[ClientApp event date]: {command.EventDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"[ClientApp event type]: {command.EventType}\n" +
                       $"[ClientApp page url]: {command.PageUrl}\n" +
                       $"[Message]: {command.Message}\n" +
                       $"[StackTrace]: {command.StackTrace}\n" +
                       $"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}\n" +
                       $"[Device]: {deviceModel} {deviceVendor} {deviceType}\n" +
                       $"[Engine]: {engineName} {engineVersion}\n" +
                       $"[O/S]: {osName} {osVersion}\n" +
                       $"[Reported UserAgent]: {command.UserAgent}\n";

        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedLogger.Verify(service => service.LogError(expected), Times.Never());
        mockedLogger.Verify(service => service.LogDebug(expected), Times.Never());
        mockedLogger.Verify(service => service.LogInformation(expected), Times.Never());
        mockedLogger.Verify(service => service.LogWarning(expected), Times.Once());
        mockedLogger.Verify(service => service.LogFatal(expected), Times.Never());
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenFatalSeverity_WhenLogMessageCommand_ShouldSucceed()
    {
        // Arrange
        var command = GetCommand("fatal");
        var browserName = command.Parsed.Browser.Name;
        var browserType = command.Parsed.Browser.Type;
        var browserVersion = command.Parsed.Browser.Version;
        var browserMajor = command.Parsed.Browser.Major;

        var deviceModel = command.Parsed.Device.Model;
        var deviceVendor = command.Parsed.Device.Vendor;
        var deviceType = command.Parsed.Device.Type;

        var engineName = command.Parsed.Engine.Name;
        var engineVersion = command.Parsed.Engine.Version;

        var osName = command.Parsed.Os.Name;
        var osVersion = command.Parsed.Os.Version;

        var expected = $"[ClientApp event date]: {command.EventDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"[ClientApp event type]: {command.EventType}\n" +
                       $"[ClientApp page url]: {command.PageUrl}\n" +
                       $"[Message]: {command.Message}\n" +
                       $"[StackTrace]: {command.StackTrace}\n" +
                       $"[Browser]: {browserName} {browserType} {browserVersion} {browserMajor}\n" +
                       $"[Device]: {deviceModel} {deviceVendor} {deviceType}\n" +
                       $"[Engine]: {engineName} {engineVersion}\n" +
                       $"[O/S]: {osName} {osVersion}\n" +
                       $"[Reported UserAgent]: {command.UserAgent}\n";

        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockedLogger.Verify(service => service.LogError(expected), Times.Never());
        mockedLogger.Verify(service => service.LogDebug(expected), Times.Never());
        mockedLogger.Verify(service => service.LogInformation(expected), Times.Never());
        mockedLogger.Verify(service => service.LogWarning(expected), Times.Never());
        mockedLogger.Verify(service => service.LogFatal(expected), Times.Once());
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenNoSeverity_WhenLogMessageCommand_ShouldFail()
    {
        // Arrange
        var command = GetCommand(string.Empty);
        var mockedLogger = new Mock<ILoggerService>();
        var databaseContext = GetTestDatabaseContext();
        var handler = new LogMessageCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<GeneralException>(() 
            => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    private LogMessageCommand GetCommand(string severity)
    {
        return new LogMessageCommand
        {
            EventDateTime = DateTimeService.Now,
            EventType = DataUtilityService.GetRandomString(),
            Severity = severity,
            Message = DataUtilityService.GetRandomString(),
            StackTrace = DataUtilityService.GetRandomString(),
            PageUrl = DataUtilityService.GetRandomString(),
            UserAgent = DataUtilityService.GetRandomString(),
            Parsed = new Parsed
            {
                Browser = new Browser
                {
                    Major = DataUtilityService.GetRandomString(),
                    Name = DataUtilityService.GetRandomString(),
                    Type = DataUtilityService.GetRandomString(),
                    Version = DataUtilityService.GetRandomString()
                },
                Cpu = new Cpu
                {
                    Architecture = DataUtilityService.GetRandomString()
                },
                Device = new Device
                {
                    Model = DataUtilityService.GetRandomString(),
                    Type = DataUtilityService.GetRandomString(),
                    Vendor = DataUtilityService.GetRandomString()
                },
                Engine = new Engine
                {
                    Name = DataUtilityService.GetRandomString(),
                    Version = DataUtilityService.GetRandomString()
                },
                Os = new Os
                {
                    Name = DataUtilityService.GetRandomString(),
                    Version = DataUtilityService.GetRandomString()
                }
            }
        };
    }
}
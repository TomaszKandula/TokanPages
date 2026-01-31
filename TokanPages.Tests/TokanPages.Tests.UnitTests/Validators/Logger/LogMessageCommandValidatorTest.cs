using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using TokanPages.Backend.Application.Logger.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Logger;

public class LogMessageCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidValues_WhenLogMessage_ShouldSucceed()
    {
        // Arrange
        var command = new LogMessageCommand
        {
            EventDateTime = DateTimeService.Now,
            EventType = DataUtilityService.GetRandomString(),
            Severity = DataUtilityService.GetRandomString(),
            Message = DataUtilityService.GetRandomString(),
            StackTrace = DataUtilityService.GetRandomString(),
            PageUrl = DataUtilityService.GetRandomString(),
            UserAgent = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new LogMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyValues_WhenLogMessage_ShouldFail()
    {
        // Arrange
        var command = new LogMessageCommand
        {
            EventDateTime = DateTimeService.Now,
            EventType = string.Empty,
            Severity = string.Empty,
            Message = string.Empty,
            StackTrace = string.Empty,
            PageUrl = string.Empty,
            UserAgent = string.Empty
        };

        // Act
        var validator = new LogMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(6);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongValues_WhenLogMessage_ShouldFail()
    {
        // Arrange
        var command = new LogMessageCommand
        {
            EventDateTime = DateTimeService.Now,
            EventType = DataUtilityService.GetRandomString(101),
            Severity = DataUtilityService.GetRandomString(101),
            Message = DataUtilityService.GetRandomString(2049),
            StackTrace = DataUtilityService.GetRandomString(4097),
            PageUrl = DataUtilityService.GetRandomString(2049),
            UserAgent = DataUtilityService.GetRandomString(226)
        };

        // Act
        var validator = new LogMessageCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(6);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_100));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_100));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_2048));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_4096));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_2048));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_225));
    }

    [Fact]
    public void GivenValidFile_WhenUploadLogFile_ShouldSucceed()
    {
        // Arrange
        var mockedConfig = GetMockSettings();
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(4096);

        var command = new UploadLogFileCommand
        {
            CatalogName = DataUtilityService.GetRandomString(),
            Data = mockFile.Object
        };

        // Act
        var validator = new UploadLogFileCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenTooLargeFile_WhenUploadLogFile_ShouldFail()
    {
        // Arrange
        var mockedConfig = GetMockSettings();
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(90128);

        var command = new UploadLogFileCommand
        {
            CatalogName = DataUtilityService.GetRandomString(),
            Data = mockFile.Object
        };

        // Act
        var validator = new UploadLogFileCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    [Fact]
    public void GivenTooLargeFileAndTooLongCatalog_WhenUploadLogFile_ShouldFail()
    {
        // Arrange
        var mockedConfig = GetMockSettings();
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(90128);

        var command = new UploadLogFileCommand
        {
            CatalogName = DataUtilityService.GetRandomString(500),
            Data = mockFile.Object
        };

        // Act
        var validator = new UploadLogFileCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_225));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    [Fact]
    public void GivenMissingFileAndCatalog_WhenUploadLogFile_ShouldFail()
    {
        // Arrange
        var mockedConfig = GetMockSettings();
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(0);

        var command = new UploadLogFileCommand
        {
            Data = mockFile.Object
        };

        // Act
        var validator = new UploadLogFileCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
}
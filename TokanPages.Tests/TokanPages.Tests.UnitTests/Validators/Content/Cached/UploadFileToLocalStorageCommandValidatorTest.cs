using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Application.Content.Cached.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Cached;

public class UploadFileToLocalStorageCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenInputFile_WhenUploadFileToLocalStorageCommand_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1;
        const string name = "Test";
        const string fileName = "TestFile.js";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);
        var command = new UploadFileToLocalStorageCommand
        {
            BinaryData = form
        };

        var mockedConfig = GetMockSettings();

        // Act
        var validator = new UploadFileToLocalStorageCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenNoInputFile_WhenUploadFileToLocalStorageCommand_ShouldFail()
    {
        // Arrange
        var command = new UploadFileToLocalStorageCommand();
        var mockedConfig = GetMockSettings();

        // Act
        var validator = new UploadFileToLocalStorageCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooBigFile_WhenUploadFileToLocalStorageCommand_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 5000;
        const string name = "Test";
        const string fileName = "TestFile.js";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);
        var command = new UploadFileToLocalStorageCommand
        {
            BinaryData = form
        };

        var mockedConfig = GetMockSettings();

        // Act
        var validator = new UploadFileToLocalStorageCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
}
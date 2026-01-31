using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Content.Assets.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Assets;

public class AddVideoAssetCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenAddVideoAssetCommand_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1;
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddVideoAssetCommand { BinaryData = form };
        var mockedConfig = GetMockSettings();

        // Act
        var validator = new AddVideoAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyFile_WhenAddVideoAssetCommand_ShouldFail()
    {
        // Arrange
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(0);
        var form = new FormFile(stream, 0, 0, name, fileName);

        var command = new AddVideoAssetCommand { BinaryData = form };
        var mockedConfig = GetMockSettings();

        // Act
        var validator = new AddVideoAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
    
    [Fact]
    public void GivenFileTooLarge_WhenAddVideoAssetCommand_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 5000;
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddVideoAssetCommand { BinaryData = form };
        var mockedConfig = GetMockSettings();

        // Act
        var validator = new AddVideoAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
}
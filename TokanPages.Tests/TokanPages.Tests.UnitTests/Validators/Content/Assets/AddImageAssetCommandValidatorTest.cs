using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Content.Assets.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Assets;

public class AddImageAssetCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenNoInputs_WhenAddImageAssetCommand_ShouldSucceed()
    {
        // Arrange
        var command = new AddImageAssetCommand();
        var mockedConfig = new Mock<IConfiguration>();
        mockedConfig
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeSingleAsset"))
            .Returns(SetReturnValue("100"));

        // Act
        var validator = new AddImageAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyFile_WhenAddImageAssetCommand_ShouldSucceed()
    {
        // Arrange
        const string name = "Test";
        const string fileName = "TestFile.txt";

        var stream = DataUtilityService.GetRandomStream(0);
        var form = new FormFile(stream, 0, 0, name, fileName);

        var command = new AddImageAssetCommand { BinaryData = form };
        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeSingleAsset"))
            .Returns(SetReturnValue("1000"));

        // Act
        var validator = new AddImageAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
    
    [Fact]
    public void GivenValidInputs_WhenAddImageAssetCommand_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1;
        const string name = "Test";
        const string fileName = "TestFile.txt";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddImageAssetCommand { BinaryData = form };
        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeSingleAsset"))
            .Returns(SetReturnValue("1000"));

        // Act
        var validator = new AddImageAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void GivenFileTooLarge_WhenAddImageAssetCommand_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 20;
        const string name = "Test";
        const string fileName = "TestFile.txt";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddImageAssetCommand { BinaryData = form };
        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeSingleAsset"))
            .Returns(SetReturnValue("10"));

        // Act
        var validator = new AddImageAssetCommandValidator(mockedConfig.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
}
using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.ApplicationSettings;
using TokanPages.Backend.Shared.ApplicationSettings.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Assets;

public class AddSingleAssetCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenRequiredFields_WhenAddSingleAsset_ShouldSucceed()
    {
        // Arrange
        var command = new AddSingleAssetCommand
        {
            MediaName = DataUtilityService.GetRandomString(50),
            MediaType = DataUtilityService.GetRandomString(50),
            Data = new byte[1024]
        };

        var validator = new AddSingleAssetCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void GivenEmptyFields_WhenAddSingleAsset_ShouldFail()
    {
        // Arrange
        var command = new AddSingleAssetCommand
        {
            MediaName = string.Empty,
            MediaType = string.Empty,
            Data = Array.Empty<byte>()
        };

        var validator = new AddSingleAssetCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
    
    [Fact]
    public void GivenInvalidLengths_WhenAddSingleAsset_ShouldFail()
    {
        var command = new AddSingleAssetCommand
        {
            MediaName = DataUtilityService.GetRandomString(300),
            MediaType = DataUtilityService.GetRandomString(300),
            Data = new byte[4096]
        };

        var validator = new AddSingleAssetCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_100));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_100));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    private static Mock<IApplicationSettings> SetupMockedSettings()
    {
        var azureStorage = new AzureStorage { MaxFileSizeSingleAsset = 2048 };
        return MockApplicationSettings(azureStorage: azureStorage);
    }
}
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class UploadUserMediaCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenUpdateUserMedia_ShouldSucceed()
    {
        // Arrange
        var command = new UploadUserMediaCommand
        {
            UserId = Guid.NewGuid(),
            MediaTarget = UserMedia.UserImage,
            MediaName = DataUtilityService.GetRandomString(),
            MediaType = DataUtilityService.GetRandomString(),
            Data = new byte[1024]
        };

        var validator = new UploadUserMediaCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenInvalidInputs_WhenUpdateUserMedia_ShouldThrowError()
    {
        // Arrange
        var command = new UploadUserMediaCommand
        {
            UserId = Guid.Empty,
            MediaTarget = UserMedia.NotSpecified,
            MediaName = string.Empty,
            MediaType = string.Empty,
            Data = Array.Empty<byte>()
        };

        var validator = new UploadUserMediaCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(5);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.NOT_SPECIFIED_MEDIA_TARGET));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenUpdateUserMedia_ShouldThrowError()
    {
        // Arrange
        var command = new UploadUserMediaCommand
        {
            UserId = Guid.NewGuid(),
            MediaTarget = UserMedia.UserImage,
            MediaName = DataUtilityService.GetRandomString(500),
            MediaType = DataUtilityService.GetRandomString(500),
            Data = new byte[4096]
        };

        var validator = new UploadUserMediaCommandValidator(SetupMockedSettings().Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_MEDIA_NAME));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_MEDIA_TYPE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    private static Mock<IConfiguration> SetupMockedSettings()
    {
        var mockedConfig = new Mock<IConfiguration>();
        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("AZ_Storage_MaxFileSizeSingleAsset"))
            .Returns(2048);

        return mockedConfig;
    }
}
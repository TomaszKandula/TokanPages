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
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
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
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    private static Mock<IConfiguration> SetupMockedSettings()
    {
        var mockedSection = new Mock<IConfigurationSection>();
        mockedSection
            .Setup(section => section.Value)
            .Returns("2048");

        var mockedConfig = new Mock<IConfiguration>();
        mockedConfig
            .Setup(c => c.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(mockedSection.Object);

        return mockedConfig;
    }
}
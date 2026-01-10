using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class RemoveUserFileCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidCommand_WhenInvokeValidation_ShouldSucceed()
    {
        // Arrange
        var command = new RemoveUserFileCommand
        {
            Type = UserFileToUpdate.Video,
            UniqueBlobName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new RemoveUserFileCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInvalidFileType_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var command = new RemoveUserFileCommand
        {
            Type = (UserFileToUpdate)900,
            UniqueBlobName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new RemoveUserFileCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
    }

    [Fact]
    public async Task GivenTooLongName_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var command = new RemoveUserFileCommand
        {
            Type = UserFileToUpdate.Video,
            UniqueBlobName = DataUtilityService.GetRandomString(500)
        };

        // Act
        var validator = new RemoveUserFileCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LENGTH_TOO_LONG_225));
    }

    [Fact]
    public async Task GivenEmptyCommand_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var command = new RemoveUserFileCommand();

        // Act
        var validator = new RemoveUserFileCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
using FluentAssertions;
using TokanPages.Backend.Application.Notifications.Mobile.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Notifications.Mobile;

public class DeleteInstallationCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidId_WhenDeleteInstallationCommand_ShouldSucceed()
    {
        // Arrange
        var command = new DeleteInstallationCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new DeleteInstallationCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyId_WhenDeleteInstallationCommand_ShouldFail()
    {
        // Arrange
        var command = new DeleteInstallationCommand
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new DeleteInstallationCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
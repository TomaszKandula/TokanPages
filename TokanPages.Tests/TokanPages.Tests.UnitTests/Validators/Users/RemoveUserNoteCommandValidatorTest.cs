using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class RemoveUserNoteCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenNoteId_WhenRemoveUserNote_ShouldSucceed()
    {
        // Arrange
        var query = new RemoveUserNoteCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new RemoveUserNoteCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyNoteId_WhenRemoveUserNote_ShouldThrowError()
    {
        // Arrange
        var query = new RemoveUserNoteCommand
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new RemoveUserNoteCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }

    [Fact]
    public void GivenNoNoteId_WhenRemoveUserNote_ShouldThrowError()
    {
        // Arrange
        var query = new RemoveUserNoteCommand();

        // Act
        var validator = new RemoveUserNoteCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
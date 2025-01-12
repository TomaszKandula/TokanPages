using FluentAssertions;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class GetUserNoteQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenNoteId_WhenGetUserNote_ShouldSucceed()
    {
        // Arrange
        var query = new GetUserNoteQuery
        {
            UserNoteId = Guid.NewGuid()
        };

        // Act
        var validator = new GetUserNoteQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyNoteId_WhenGetUserNote_ShouldThrowError()
    {
        // Arrange
        var query = new GetUserNoteQuery
        {
            UserNoteId = Guid.Empty
        };

        // Act
        var validator = new GetUserNoteQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }

    [Fact]
    public void GivenNoNoteId_WhenGetUserNote_ShouldThrowError()
    {
        // Arrange
        var query = new GetUserNoteQuery();

        // Act
        var validator = new GetUserNoteQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
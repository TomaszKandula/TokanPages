namespace TokanPages.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Users;

public class GetUserQueryValidatorTest
{
    [Fact]
    public void GivenCorrectId_WhenGetUser_ShouldFinishSuccessful()
    {
        // Arrange
        var getUserQuery = new GetUserQuery
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new GetUserQueryValidator();
        var result = validator.Validate(getUserQuery);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenGetUser_ShouldThrowError()
    {
        // Arrange
        var getUserQuery = new GetUserQuery
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new GetUserQueryValidator();
        var result = validator.Validate(getUserQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
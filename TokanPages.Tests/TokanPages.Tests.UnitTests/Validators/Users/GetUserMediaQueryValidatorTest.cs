namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using System;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Users;

public class GetUserMediaQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenGetMediaUser_ShouldSucceed()
    {
        // Arrange
        var query = new GetUserMediaQuery
        {
            Id = Guid.NewGuid(),
            BlobName = DataUtilityService.GetRandomString()
        };

        var validator = new GetUserMediaQueryValidator();

        // Act
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenNoInputs_WhenGetMediaUser_ShouldThrowError()
    {
        // Arrange
        var query = new GetUserMediaQuery();
        var validator = new GetUserMediaQueryValidator();

        // Act
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongString_WhenGetMediaUser_ShouldThrowError()
    {
        // Arrange
        var query = new GetUserMediaQuery
        {
            Id = Guid.NewGuid(),
            BlobName = DataUtilityService.GetRandomString(500)
        };

        var validator = new GetUserMediaQueryValidator();

        // Act
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.NAME_TOO_LONG));
    }
}
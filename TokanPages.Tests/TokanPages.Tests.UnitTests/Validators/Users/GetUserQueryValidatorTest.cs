﻿using FluentAssertions;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class GetUserQueryValidatorTest
{
    [Fact]
    public void GivenValidId_WhenGetUser_ShouldSucceed()
    {
        // Arrange
        var query = new GetUserQuery { Id = Guid.NewGuid() };

        // Act
        var validator = new GetUserQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenGetUser_ShouldThrowError()
    {
        // Arrange
        var query = new GetUserQuery { Id = Guid.Empty };

        // Act
        var validator = new GetUserQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
using FluentAssertions;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class GetUserFileListQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenCorrectQuery_WhenInvokeValidation_ShouldSucceed()
    {
        // Arrange
        var query = new GetUserFileListQuery
        {
            Type = UserFileToReceive.Any
        };

        // Act
        var validator = new GetUserFileListQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInvalidQuery_WhenInvokeValidation_ShouldSucceed()
    {
        // Arrange
        var query = new GetUserFileListQuery
        {
            Type = (UserFileToReceive)999
        };

        // Act
        var validator = new GetUserFileListQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
    }

    [Fact]
    public async Task GivenEmptyQuery_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var query = new GetUserFileListQuery();

        // Act
        var validator = new GetUserFileListQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
    }
}
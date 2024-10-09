using FluentAssertions;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Components;

public class GetContentQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenGetContent_ShouldSucceed()
    {
        // Arrange
        var query = new GetContentQuery
        {
            ContentName = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenGetContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetContentQuery
        {
            ContentName = string.Empty
        };

        // Act
        var validator = new GetContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
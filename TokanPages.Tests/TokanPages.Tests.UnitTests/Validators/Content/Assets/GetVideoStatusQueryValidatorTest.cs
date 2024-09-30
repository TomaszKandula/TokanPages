using FluentAssertions;
using TokanPages.Backend.Application.Content.Assets.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Assets;

public class GetVideoStatusQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenGetVideoStatusQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetVideoStatusQuery
        {
            TicketId = Guid.NewGuid()
        };

        // Act
        var validator = new GetVideoStatusQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GivenEmptyInput_WhenGetVideoStatusQuery_ShouldFail()
    {
        // Arrange
        var query = new GetVideoStatusQuery
        {
            TicketId = Guid.Empty
        };

        // Act
        var validator = new GetVideoStatusQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
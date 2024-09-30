using FluentAssertions;
using TokanPages.Backend.Application.Notifications.Mobile.Query;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Notifications.Mobile;

public class GetInstallationQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidId_WhenGetInstallationQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetInstallationQuery
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new GetInstallationQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyId_WhenGetInstallationQuery_ShouldFail()
    {
        // Arrange
        var query = new GetInstallationQuery
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new GetInstallationQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
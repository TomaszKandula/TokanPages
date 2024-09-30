using FluentAssertions;
using TokanPages.Backend.Application.Invoicing.Batches.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Batches;

public class GetBatchProcessingStatusQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidKey_WhenGetBatchProcessingStatus_ShouldSucceed()
    {
        // Arrange
        var query = new GetBatchProcessingStatusQuery
        {
            ProcessBatchKey = Guid.NewGuid()
        };

        // Act
        var validator = new GetBatchProcessingStatusQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyKey_WhenGetBatchProcessingStatus_ShouldFail()
    {
        // Arrange
        var query = new GetBatchProcessingStatusQuery
        {
            ProcessBatchKey = Guid.Empty
        };

        // Act
        var validator = new GetBatchProcessingStatusQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
using FluentAssertions;
using TokanPages.Backend.Application.Invoicing.Batches.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Batches;

public class GetIssuedBatchInvoiceQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenInvoiceNumber_WhenGetIssuedBatchInvoice_ShouldSucceed()
    {
        // Arrange
        var query = new GetIssuedBatchInvoiceQuery
        {
            InvoiceNumber = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetIssuedBatchInvoiceQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyInvoiceNumber_WhenGetIssuedBatchInvoice_ShouldFail()
    {
        // Arrange
        var query = new GetIssuedBatchInvoiceQuery
        {
            InvoiceNumber = string.Empty
        };

        // Act
        var validator = new GetIssuedBatchInvoiceQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
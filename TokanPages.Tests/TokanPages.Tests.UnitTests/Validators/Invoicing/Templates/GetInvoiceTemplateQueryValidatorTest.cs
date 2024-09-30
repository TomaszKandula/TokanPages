using FluentAssertions;
using TokanPages.Backend.Application.Invoicing.Templates.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Templates;

public class GetInvoiceTemplateQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenGetInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        var query = new GetInvoiceTemplateQuery
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new GetInvoiceTemplateQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyInput_WhenGetInvoiceTemplate_ShouldFail()
    {
        // Arrange
        var query = new GetInvoiceTemplateQuery
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new GetInvoiceTemplateQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
using FluentAssertions;
using TokanPages.Backend.Application.Invoicing.Batches.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Batches;

public class OrderInvoiceBatchCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidIds_WhenOrderInvoiceBatch_ShouldSucceed()
    {
        // Arrange
        var command = new OrderInvoiceBatchCommand
        {
            UserId = Guid.NewGuid(),
            UserBankAccountId = Guid.NewGuid(),
            UserCompanyId = Guid.NewGuid()
        };

        // Act
        var validator = new OrderInvoiceBatchCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyIds_WhenOrderInvoiceBatch_ShouldFail()
    {
        // Arrange
        var command = new OrderInvoiceBatchCommand
        {
            UserId = Guid.Empty,
            UserBankAccountId = Guid.Empty,
            UserCompanyId = Guid.Empty
        };

        // Act
        var validator = new OrderInvoiceBatchCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}
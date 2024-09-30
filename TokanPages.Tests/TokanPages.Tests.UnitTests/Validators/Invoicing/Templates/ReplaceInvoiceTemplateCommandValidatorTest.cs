using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Application.Invoicing.Templates.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Templates;

public class ReplaceInvoiceTemplateCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidFile_WhenReplaceInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1;
        const string name = "Test";
        const string fileName = "TestFile.html";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new ReplaceInvoiceTemplateCommand
        {
            Id = Guid.NewGuid(),
            Data = form,
            Description = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new ReplaceInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenFileTooLarge_WhenReplaceInvoiceTemplate_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 5 * 1024 * 1024;
        const string name = "Test";
        const string fileName = "TestFile.html";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new ReplaceInvoiceTemplateCommand
        {
            Id = Guid.NewGuid(),
            Data = form,
            Description = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new ReplaceInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
    
    [Fact]
    public async Task GivenEmptyFile_WhenReplaceInvoiceTemplate_ShouldFail()
    {
        // Arrange
        var command = new ReplaceInvoiceTemplateCommand
        {
            Id = Guid.Empty,
            Data = null,
            Description = string.Empty
        };

        // Act
        var validator = new ReplaceInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}
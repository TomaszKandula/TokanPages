using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Application.Invoicing.Templates.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Templates;

public class AddInvoiceTemplateCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenTemplateFile_WhenAddInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1;
        const string name = "Test";
        const string fileName = "TestFile.html";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddInvoiceTemplateCommand
        {
            Data = form,
            Description = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyFile_WhenAddInvoiceTemplate_ShouldFail()
    {
        // Arrange
        var command = new AddInvoiceTemplateCommand
        {
            Data = null,
            Description = string.Empty
        };

        // Act
        var validator = new AddInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public async Task GivenTemplateFileTooLarge_WhenAddInvoiceTemplate_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 5 * 1024 * 1024;
        const string name = "Test";
        const string fileName = "TestFile.html";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var form = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var command = new AddInvoiceTemplateCommand
        {
            Data = form,
            Description = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddInvoiceTemplateCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }
}
using FluentAssertions;
using TokanPages.Backend.Application.Invoicing.Templates.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Invoicing.Templates;

public class RemoveInvoiceTemplateCommandValidatorTest : TestBase
{
   [Fact]
   public async Task GivenValidId_WhenRemoveInvoiceTemplate_ShouldSucceed()
   {
      // Arrange
      var command = new RemoveInvoiceTemplateCommand
      {
         Id = Guid.NewGuid()
      };

      // Act
      var validator = new RemoveInvoiceTemplateCommandValidator();
      var result = await validator.ValidateAsync(command);

      // Assert
      result.Errors.Should().BeEmpty();
   }

   [Fact]
   public async Task GivenEmptyId_WhenRemoveInvoiceTemplate_ShouldFail()
   {
      // Arrange
      var command = new RemoveInvoiceTemplateCommand
      {
         Id = Guid.Empty
      };

      // Act
      var validator = new RemoveInvoiceTemplateCommandValidator();
      var result = await validator.ValidateAsync(command);

      // Assert
      result.Errors.Count.Should().Be(1);
      result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
   }
}
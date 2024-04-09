using FluentAssertions;
using Moq;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.TemplateService;
using TokanPages.Services.TemplateService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class TemplateServiceTest : TestBase
{
    [Fact]
    public async Task GivenValidTemplateId_WhenGetInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        const int templateDataLength = 2048;
        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = false
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.GetInvoiceTemplate(templates[0].Id);

        // Assert
        result.ContentData.Should().HaveCount(templateDataLength);
    }

    [Fact]
    public async Task GivenInvalidTemplateId_WhenGetInvoiceTemplate_ShouldThrowError()
    {
        // Arrange
        const int templateDataLength = 1024;
        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = false
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.GetInvoiceTemplate(Guid.NewGuid()));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_TEMPLATE_ID));
    }

    [Fact]
    public async Task GivenDeletedTemplateId_WhenGetInvoiceTemplate_ShouldThrowError()
    {
        // Arrange
        const int templateDataLength = 1024;
        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = true
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.GetInvoiceTemplate(templates[1].Id));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_TEMPLATE_ID));
    }

    [Fact]
    public async Task GivenExistingTemplateId_WhenRemoveInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        const int templateDataLength = 2048;
        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = false
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        await service.RemoveInvoiceTemplate(templates[0].Id);

        // Assert
        var result = databaseContext.InvoiceTemplates
            .Where(invoiceTemplates => invoiceTemplates.Id == templates[0].Id)
            .Select(invoiceTemplates => invoiceTemplates.IsDeleted)
            .FirstOrDefault();

        result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenNonExistingTemplateId_WhenRemoveInvoiceTemplate_ShouldThrowError()
    {
        // Arrange
        const int templateDataLength = 2048;
        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = true
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = true
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.RemoveInvoiceTemplate(Guid.NewGuid()));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_TEMPLATE_ID));
    }

    [Fact]
    public async Task GivenInvoiceTemplateData_WhenAddInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        var template = new InvoiceTemplate
        {
            TemplateName = DataUtilityService.GetRandomString(),
            InvoiceTemplateData = new InvoiceTemplateData
            {
                ContentData = new byte[2048],
                ContentType = DataUtilityService.GetRandomString()
            },
            InvoiceTemplateDescription = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.AddInvoiceTemplate(template);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenExistingInvoiceTemplateId_WhenReplaceInvoiceTemplate_ShouldSucceed()
    {
        // Arrange
        const int templateDataLength = 1024;
        var newTemplateData = new InvoiceTemplateData
        {
            ContentData = new byte[514],
            ContentType = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(useAlphabetOnly: true)
        };

        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = false
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        await service.ReplaceInvoiceTemplate(templates[0].Id, newTemplateData);

        // Assert
        var result = databaseContext.InvoiceTemplates
            .FirstOrDefault(invoiceTemplates => invoiceTemplates.Id == templates[0].Id);

        result.Should().NotBeNull();
        result?.Data.Should().HaveCount(newTemplateData.ContentData.Length);
    }

    [Fact]
    public async Task GivenNonExistingInvoiceTemplateId_WhenReplaceInvoiceTemplate_ShouldThrowError()
    {
        // Arrange
        const int templateDataLength = 1024;
        var newTemplateData = new InvoiceTemplateData
        {
            ContentData = new byte[514],
            ContentType = DataUtilityService.GetRandomString()
        };

        var templates = new List<InvoiceTemplates>
        {
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-150),
                IsDeleted = false
            },
            new()
            {
                Name = DataUtilityService.GetRandomString(),
                Data = new byte[templateDataLength],
                ContentType = DataUtilityService.GetRandomString(),
                ShortDescription = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-100),
                IsDeleted = false
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(templates);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var service = new TemplateService(
            databaseContext, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.ReplaceInvoiceTemplate(Guid.NewGuid(), newTemplateData));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_TEMPLATE_ID));
    }
}
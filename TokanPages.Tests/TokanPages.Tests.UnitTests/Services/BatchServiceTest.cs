using Moq;
using Xunit;
using FluentAssertions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.BatchService;
using TokanPages.Services.BatchService.Models;

namespace TokanPages.Tests.UnitTests.Services;

public class BatchServiceTest : TestBase
{
    [Fact]
    public async Task GivenValidInvoiceNUmber_WhenGetIssuedInvoice_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var invoices = new List<IssuedInvoices>
        {
            new()
            {
                UserId = user.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                InvoiceData = new byte[4096],
                ContentType = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-12)
            },
            new()
            {
                UserId = user.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                InvoiceData = new byte[2048],
                ContentType = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-6)
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddRangeAsync(invoices);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLoggerService = new Mock<ILoggerService>();

        var service = new BatchService(
            databaseContext, 
            mockedDateTimeService.Object, 
            mockedLoggerService.Object);

        // Act
        var result = await service.GetIssuedInvoice(invoices[0].InvoiceNumber);

        // Assert
        result.Number.Should().Be(invoices[0].InvoiceNumber);
    }

    [Fact]
    public async Task GivenInvalidInvoiceNUmber_WhenGetIssuedInvoice_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var invoices = new List<IssuedInvoices>
        {
            new()
            {
                UserId = user.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                InvoiceData = new byte[4096],
                ContentType = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-12)
            },
            new()
            {
                UserId = user.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                InvoiceData = new byte[2048],
                ContentType = DataUtilityService.GetRandomString(),
                GeneratedAt = DateTimeService.Now.AddDays(-6)
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddRangeAsync(invoices);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLoggerService = new Mock<ILoggerService>();

        var service = new BatchService(
            databaseContext, 
            mockedDateTimeService.Object, 
            mockedLoggerService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.GetIssuedInvoice(DataUtilityService.GetRandomString()));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_INVOICE_NUMBER));
    }

    [Fact]
    public async Task GivenOrderList_WhenOrderInvoiceBatchProcessing_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userCompany = new UserCompanies
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(60),
            VatNumber = DataUtilityService.GetRandomString(25),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            PhoneNumber = DataUtilityService.GetRandomString(11),
            StreetAddress = DataUtilityService.GetRandomString(25),
            PostalCode = DataUtilityService.GetRandomString(6),
            City = DataUtilityService.GetRandomString(7),
            CurrencyCode = CurrencyCodes.Dkk,
            CountryCode = CountryCodes.Denmark
        };

        var userBankAccount = new UserBankAccounts
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            BankName = DataUtilityService.GetRandomString(10),
            SwiftNumber = DataUtilityService.GetRandomString(11),
            AccountNumber = DataUtilityService.GetRandomString(28),
            CurrencyCode = CurrencyCodes.Dkk
        };

        var orders = new List<OrderDetail>
        {
            new()
            {
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CompanyName = DataUtilityService.GetRandomString(),
                CompanyVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                CurrencyCode = CurrencyCodes.Eur,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        ItemText = DataUtilityService.GetRandomString(),
                        ItemQuantity = DataUtilityService.GetRandomInteger(),
                        ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                        ItemAmount = DataUtilityService.GetRandomDecimal(),
                        ItemDiscountRate = 0,
                        ValueAmount = DataUtilityService.GetRandomDecimal(),
                        VatRate = 0,
                        GrossAmount = DataUtilityService.GetRandomDecimal(),
                        CurrencyCode = CurrencyCodes.Gbp
                    },
                    new()
                    {
                        ItemText = DataUtilityService.GetRandomString(),
                        ItemQuantity = DataUtilityService.GetRandomInteger(),
                        ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                        ItemAmount = DataUtilityService.GetRandomDecimal(),
                        ItemDiscountRate = 0,
                        ValueAmount = DataUtilityService.GetRandomDecimal(),
                        VatRate = 0,
                        GrossAmount = DataUtilityService.GetRandomDecimal(),
                        CurrencyCode = CurrencyCodes.Gbp
                    }
                }
            },                
            new()
            {
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CompanyName = DataUtilityService.GetRandomString(),
                CompanyVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                CurrencyCode = CurrencyCodes.Eur,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        ItemText = DataUtilityService.GetRandomString(),
                        ItemQuantity = DataUtilityService.GetRandomInteger(),
                        ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                        ItemAmount = DataUtilityService.GetRandomDecimal(),
                        ItemDiscountRate = 0,
                        ValueAmount = DataUtilityService.GetRandomDecimal(),
                        VatRate = 0,
                        GrossAmount = DataUtilityService.GetRandomDecimal(),
                        CurrencyCode = CurrencyCodes.Gbp
                    },
                    new()
                    {
                        ItemText = DataUtilityService.GetRandomString(),
                        ItemQuantity = DataUtilityService.GetRandomInteger(),
                        ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                        ItemAmount = DataUtilityService.GetRandomDecimal(),
                        ItemDiscountRate = 0,
                        ValueAmount = DataUtilityService.GetRandomDecimal(),
                        VatRate = 0,
                        GrossAmount = DataUtilityService.GetRandomDecimal(),
                        CurrencyCode = CurrencyCodes.Gbp
                    }
                }
            }                
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(userCompany);
        await databaseContext.AddAsync(userBankAccount);
        await databaseContext.SaveChangesAsync();
            
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLoggerService = new Mock<ILoggerService>();
            
        var service = new BatchService(
            databaseContext, 
            mockedDateTimeService.Object, 
            mockedLoggerService.Object);

        // Act
        var result = await service.OrderInvoiceBatchProcessing(orders);

        // Assert
        result.Should().NotBeEmpty();

        databaseContext.BatchInvoices.ToList().Count.Should().Be(2);
        databaseContext.BatchInvoiceItems.ToList().Count.Should().Be(4);
        databaseContext.BatchInvoicesProcessing.ToList().Count.Should().Be(1);
    }

    [Fact]
    public async Task GivenValidBatchProcessingKey_WhenGetBatchInvoiceProcessingStatus_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var processing = new BatchInvoicesProcessing
        {
            Id = Guid.NewGuid(),
            BatchProcessingTime = null,
            Status = ProcessingStatuses.New,
            CreatedAt = DateTimeService.Now
        };

        var userCompany = new UserCompanies
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(60),
            VatNumber = DataUtilityService.GetRandomString(25),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            PhoneNumber = DataUtilityService.GetRandomString(11),
            StreetAddress = DataUtilityService.GetRandomString(25),
            PostalCode = DataUtilityService.GetRandomString(6),
            City = DataUtilityService.GetRandomString(7),
            CurrencyCode = CurrencyCodes.Dkk,
            CountryCode = CountryCodes.Denmark
        };

        var userBankAccount = new UserBankAccounts
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            BankName = DataUtilityService.GetRandomString(10),
            SwiftNumber = DataUtilityService.GetRandomString(11),
            AccountNumber = DataUtilityService.GetRandomString(28),
            CurrencyCode = CurrencyCodes.Dkk
        };

        var invoices = new List<BatchInvoices>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CustomerName = DataUtilityService.GetRandomString(),
                CustomerVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                CreatedAt = DateTimeService.Now,
                CreatedBy = user.Id,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = processing.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CustomerName = DataUtilityService.GetRandomString(),
                CustomerVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                CreatedAt = DateTimeService.Now,
                CreatedBy = user.Id,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = processing.Id
            }
        };

        var invoiceItems = new List<BatchInvoiceItems>
        {
            new()
            {
                BatchInvoiceId = invoices[0].Id,
                ItemText = DataUtilityService.GetRandomString(),
                ItemQuantity = DataUtilityService.GetRandomInteger(),
                ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                ItemAmount = DataUtilityService.GetRandomDecimal(),
                ItemDiscountRate = null,
                ValueAmount = DataUtilityService.GetRandomDecimal(),
                VatRate = null,
                GrossAmount = DataUtilityService.GetRandomDecimal(),
                CurrencyCode = CurrencyCodes.Gbp
            },
            new()
            {
                BatchInvoiceId = invoices[1].Id,
                ItemText = DataUtilityService.GetRandomString(),
                ItemQuantity = DataUtilityService.GetRandomInteger(),
                ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                ItemAmount = DataUtilityService.GetRandomDecimal(),
                ItemDiscountRate = null,
                ValueAmount = DataUtilityService.GetRandomDecimal(),
                VatRate = null,
                GrossAmount = DataUtilityService.GetRandomDecimal(),
                CurrencyCode = CurrencyCodes.Gbp
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(userCompany);
        await databaseContext.AddAsync(userBankAccount);
        await databaseContext.AddAsync(processing);
        await databaseContext.AddRangeAsync(invoices);
        await databaseContext.AddRangeAsync(invoiceItems);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLoggerService = new Mock<ILoggerService>();
            
        var service = new BatchService(
            databaseContext, 
            mockedDateTimeService.Object, 
            mockedLoggerService.Object);

        // Act
        var result = await service.GetBatchInvoiceProcessingStatus(processing.Id);

        // Assert
        result.Status.Should().Be(ProcessingStatuses.New);
        //result.BatchProcessingTime.Should().BeNull();
    }

    [Fact]
    public async Task GivenInvalidBatchProcessingKey_WhenGetBatchInvoiceProcessingStatus_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var processing = new BatchInvoicesProcessing
        {
            Id = Guid.NewGuid(),
            BatchProcessingTime = null,
            Status = ProcessingStatuses.New,
            CreatedAt = DateTimeService.Now
        };

        var userCompany = new UserCompanies
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(60),
            VatNumber = DataUtilityService.GetRandomString(25),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            PhoneNumber = DataUtilityService.GetRandomString(11),
            StreetAddress = DataUtilityService.GetRandomString(25),
            PostalCode = DataUtilityService.GetRandomString(6),
            City = DataUtilityService.GetRandomString(7),
            CurrencyCode = CurrencyCodes.Dkk,
            CountryCode = CountryCodes.Denmark
        };

        var userBankAccount = new UserBankAccounts
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            BankName = DataUtilityService.GetRandomString(10),
            SwiftNumber = DataUtilityService.GetRandomString(11),
            AccountNumber = DataUtilityService.GetRandomString(28),
            CurrencyCode = CurrencyCodes.Dkk
        };

        var invoices = new List<BatchInvoices>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CustomerName = DataUtilityService.GetRandomString(),
                CustomerVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                CreatedAt = DateTimeService.Now,
                CreatedBy = user.Id,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = processing.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                UserCompanyId = userCompany.Id,
                UserBankAccountId = userBankAccount.Id,
                InvoiceNumber = DataUtilityService.GetRandomString(),
                VoucherDate = DataUtilityService.GetRandomDateTime(),
                ValueDate = DataUtilityService.GetRandomDateTime(),
                DueDate = DataUtilityService.GetRandomDateTime(),
                PaymentTerms = DataUtilityService.GetRandomInteger(),
                PaymentType = PaymentTypes.CreditCard,
                CustomerName = DataUtilityService.GetRandomString(),
                CustomerVatNumber = DataUtilityService.GetRandomString(),
                CountryCode = CountryCodes.Poland,
                City = DataUtilityService.GetRandomString(),
                StreetAddress = DataUtilityService.GetRandomString(),
                PostalCode = DataUtilityService.GetRandomString(),
                PostalArea = DataUtilityService.GetRandomString(),
                InvoiceTemplateName = DataUtilityService.GetRandomString(),
                CreatedAt = DateTimeService.Now,
                CreatedBy = user.Id,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = processing.Id
            }
        };

        var invoiceItems = new List<BatchInvoiceItems>
        {
            new()
            {
                BatchInvoiceId = invoices[0].Id,
                ItemText = DataUtilityService.GetRandomString(),
                ItemQuantity = DataUtilityService.GetRandomInteger(),
                ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                ItemAmount = DataUtilityService.GetRandomDecimal(),
                ItemDiscountRate = null,
                ValueAmount = DataUtilityService.GetRandomDecimal(),
                VatRate = null,
                GrossAmount = DataUtilityService.GetRandomDecimal(),
                CurrencyCode = CurrencyCodes.Gbp
            },
            new()
            {
                BatchInvoiceId = invoices[1].Id,
                ItemText = DataUtilityService.GetRandomString(),
                ItemQuantity = DataUtilityService.GetRandomInteger(),
                ItemQuantityUnit = DataUtilityService.GetRandomString(3),
                ItemAmount = DataUtilityService.GetRandomDecimal(),
                ItemDiscountRate = null,
                ValueAmount = DataUtilityService.GetRandomDecimal(),
                VatRate = null,
                GrossAmount = DataUtilityService.GetRandomDecimal(),
                CurrencyCode = CurrencyCodes.Gbp
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(userCompany);
        await databaseContext.AddAsync(userBankAccount);
        await databaseContext.AddAsync(processing);
        await databaseContext.AddRangeAsync(invoices);
        await databaseContext.AddRangeAsync(invoiceItems);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLoggerService = new Mock<ILoggerService>();

        var service = new BatchService(
            databaseContext, 
            mockedDateTimeService.Object, 
            mockedLoggerService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => service.GetBatchInvoiceProcessingStatus(Guid.NewGuid()));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_PROCESSING_BATCH_KEY));
    }
}
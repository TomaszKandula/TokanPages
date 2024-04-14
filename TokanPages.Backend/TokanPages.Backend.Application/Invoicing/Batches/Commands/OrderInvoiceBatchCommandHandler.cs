using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.BatchService;
using TokanPages.Services.BatchService.Models;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.VatService;
using TokanPages.Services.VatService.Models;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

public class OrderInvoiceBatchCommandHandler : RequestHandler<OrderInvoiceBatchCommand, OrderInvoiceBatchCommandResult>
{
    private readonly IBatchService _batchService;

    private readonly IVatService _vatService;

    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public OrderInvoiceBatchCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IBatchService batchService, 
        IVatService vatService, IUserService userService, IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _batchService = batchService;
        _vatService = vatService;
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<OrderInvoiceBatchCommandResult> Handle(OrderInvoiceBatchCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var userId = user.Id;
        LoggerService.LogInformation($"Request to process {request.OrderDetails.Count()} orders. User ID: {userId}");

        var vatOptions = new PolishVatNumberOptions(true, true);
        var vatPatterns = await DatabaseContext.VatNumberPatterns
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        LoggerService.LogInformation($"Found {vatPatterns.Count} VAT patterns");

        var availableTemplates = await DatabaseContext.InvoiceTemplates
            .AsNoTracking()
            .Where(templates => !templates.IsDeleted)
            .ToListAsync(cancellationToken);
            
        LoggerService.LogInformation($"Found {availableTemplates.Count} invoice patterns");

        var order = new List<OrderDetail>();
        foreach (var orderDetails in request.OrderDetails)
        {
            var vatCheckRequest = new VatValidationRequest(orderDetails.CompanyVatNumber, vatPatterns, vatOptions);
            var vatCheckResult = _vatService.ValidateVatNumber(vatCheckRequest);

            if (!vatCheckResult.IsValid)
                throw new ValidationException(vatCheckResult, ValidationCodes.INVALID_VAT);

            LoggerService.LogInformation($"VAT has been checked successfully. Begin processing {order.Count} orders");
            
            var items = new List<InvoiceItem>();
            foreach (var item in orderDetails.InvoiceItems)
            {
                var valueAmount = GetValueAmount(item.ItemQuantity, item.ItemAmount, item.ItemDiscountRate);
                var grossAmount = GetGrossAmount(valueAmount, item.VatRate);

                items.Add(new InvoiceItem
                {
                    ItemText = item.ItemText,
                    ItemQuantity = item.ItemQuantity,
                    ItemQuantityUnit = item.ItemQuantityUnit,
                    ItemAmount = item.ItemAmount,
                    ItemDiscountRate = item.ItemDiscountRate,
                    ValueAmount = valueAmount,
                    VatRate = item.VatRate,
                    GrossAmount = grossAmount,
                    CurrencyCode = item.CurrencyCode
                });
            }

            CheckVoucherDateAndValueDate(orderDetails.VoucherDate, orderDetails.ValueDate);

            var voucherDate = orderDetails.VoucherDate ?? _dateTimeService.Now;
            var valueDate = orderDetails.ValueDate ?? _dateTimeService.Now;

            var isTemplateExists = availableTemplates.Any(templates => templates.Name == orderDetails.InvoiceTemplateName);
            if (!isTemplateExists)
                throw new BusinessException(nameof(ErrorCodes.INVALID_TEMPLATE_NAME), ErrorCodes.INVALID_TEMPLATE_NAME);

            LoggerService.LogInformation($"Order has been processed with {items.Count} invoice item(s)");
            
            order.Add(new OrderDetail
            {
                UserId = userId,
                UserCompanyId = request.UserCompanyId,
                UserBankAccountId = request.UserBankAccountId,
                InvoiceNumber = Guid.NewGuid().ToString("N"),
                VoucherDate = voucherDate,
                ValueDate = valueDate,
                DueDate = valueDate.AddDays(orderDetails.PaymentTerms),
                PaymentTerms = orderDetails.PaymentTerms,
                PaymentType = orderDetails.PaymentType,
                PaymentStatus = orderDetails.PaymentStatus,
                CompanyName = orderDetails.CompanyName,
                CompanyVatNumber = orderDetails.CompanyVatNumber,
                CountryCode = orderDetails.CountryCode,
                CurrencyCode = orderDetails.CurrencyCode,
                City = orderDetails.City,
                StreetAddress = orderDetails.StreetAddress,
                PostalCode = orderDetails.PostalCode,
                PostalArea = orderDetails.PostalArea,
                InvoiceTemplateName = orderDetails.InvoiceTemplateName,
                InvoiceItems = items
            });
        }

        var result = await _batchService.OrderInvoiceBatchProcessing(order, cancellationToken);
        return new OrderInvoiceBatchCommandResult
        {
            ProcessBatchKey = result
        };
    }

    private static decimal GetValueAmount(int quantity, decimal amount, decimal? discountRate)
    {
        var @base = quantity * amount;
        var discount = discountRate != null 
            ? @base * (decimal)discountRate 
            : 0.0m;
        return @base - discount;
    }

    private static decimal GetGrossAmount(decimal amount, decimal? vatRate)
    {
        return vatRate == null 
            ? amount 
            : amount * (1 + (decimal)vatRate);
    }

    private static void CheckVoucherDateAndValueDate(DateTime? voucherDate, DateTime? valueDate)
    {
        if (voucherDate is null && valueDate is not null)
            throw new BusinessException(nameof(ErrorCodes.DATE_TYPE_MISMATCH), ErrorCodes.DATE_TYPE_MISMATCH);

        if (voucherDate is not null && valueDate is null)
            throw new BusinessException(nameof(ErrorCodes.DATE_TYPE_MISMATCH), ErrorCodes.DATE_TYPE_MISMATCH);
    }
}
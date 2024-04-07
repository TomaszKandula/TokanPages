using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.BatchService.Models;

namespace TokanPages.Services.BatchService;

public class BatchService : IBatchService
{
    private readonly DatabaseContext _databaseContext;

    private readonly IDateTimeService _dateTimeService;
        
    private readonly ILoggerService _loggerService;

    public BatchService(DatabaseContext databaseContext, IDateTimeService dateTimeService, ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Place an order for invoice processing. 
    /// </summary>
    /// <param name="orderDetails">Desired invoice data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Process Batch Key.</returns>
    public async Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails, CancellationToken cancellationToken = default)
    {
        var invoices = new List<BatchInvoices>();
        var invoiceItems = new List<BatchInvoiceItems>();

        var processing = new BatchInvoicesProcessing
        {
            Id = Guid.NewGuid(),
            BatchProcessingTime = null,
            Status = ProcessingStatuses.New,
            CreatedAt = _dateTimeService.Now
        };

        foreach (var order in orderDetails)
        {
            var batchInvoiceId = Guid.NewGuid();
            invoices.Add(new BatchInvoices
            {
                Id = batchInvoiceId,
                InvoiceNumber = order.InvoiceNumber,
                VoucherDate = order.VoucherDate ?? DateTime.Now,
                ValueDate = order.ValueDate ?? DateTime.Now,
                DueDate = order.DueDate,
                PaymentTerms = order.PaymentTerms,
                PaymentType = order.PaymentType,
                PaymentStatus = order.PaymentStatus,
                CustomerName = order.CompanyName,
                CustomerVatNumber = order.CompanyVatNumber,
                CountryCode = order.CountryCode,
                City = order.City,
                StreetAddress = order.StreetAddress,
                PostalCode = order.PostalCode,
                PostalArea = order.PostalArea,
                InvoiceTemplateName = order.InvoiceTemplateName,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = order.UserId,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = processing.Id,
                UserId = order.UserId,
                UserCompanyId = order.UserCompanyId,
                UserBankAccountId = order.UserBankAccountId
            });

            foreach (var item in order.InvoiceItems)
            {
                invoiceItems.Add(new BatchInvoiceItems
                {
                    Id = Guid.NewGuid(),
                    BatchInvoiceId = batchInvoiceId,
                    ItemText = item.ItemText,
                    ItemQuantity = item.ItemQuantity,
                    ItemQuantityUnit = item.ItemQuantityUnit,
                    ItemAmount = item.ItemAmount,
                    ItemDiscountRate = item.ItemDiscountRate,
                    ValueAmount = item.ValueAmount,
                    VatRate = item.VatRate,
                    GrossAmount = item.GrossAmount,
                    CurrencyCode = item.CurrencyCode
                });
            }
        }

        await _databaseContext.AddAsync(processing, cancellationToken);
        await _databaseContext.AddRangeAsync(invoices, cancellationToken);
        await _databaseContext.AddRangeAsync(invoiceItems, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var messageText1 = $"Invoice batch processing has been ordered (ProcessBatchKey: {processing.Id}).";
        var messageText2 = $"Total invoices: {invoices.Count}.";

        _loggerService.LogInformation($"{messageText1} {messageText2}.");
        return processing.Id;
    }

    /// <summary>
    /// Processes all outstanding invoices that have status 'new'.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default)
    {
        var processingList = await _databaseContext.BatchInvoicesProcessing
            .AsNoTracking()
            .Where(processing => processing.Status == ProcessingStatuses.New)
            .Select(processing => processing.Id)
            .ToListAsync(cancellationToken);

        if (!processingList.Any())
        {
            _loggerService.LogInformation("No new invoices to process.");
            return;
        }

        var (invoices, invoiceItemsList, invoiceTemplates) = await GetInvoiceData(processingList, cancellationToken);
        var (userCompaniesList, userBankAccountsList) = await GetUserData(invoices, cancellationToken);
        var issuedInvoices = new List<IssuedInvoices>();

        foreach (var invoice in invoices)
        {
            var timer = new Stopwatch();
            var processing = await _databaseContext.BatchInvoicesProcessing
                .Where(processing => processing.Id == invoice.ProcessBatchKey)
                .SingleOrDefaultAsync(cancellationToken);

            if (processing is null)
                throw new BusinessException(nameof(ErrorCodes.PROCESSING_EXCEPTION), ErrorCodes.PROCESSING_EXCEPTION);

            await LogProcessingStarted(invoice, processing, timer, cancellationToken);
            try
            {
                var templateData = invoiceTemplates
                    .Where(templates => templates.Name == invoice.InvoiceTemplateName)
                    .Select(templates => templates.Data)
                    .SingleOrDefault();

                if (templateData is null)
                    throw new BusinessException(nameof(ErrorCodes.MISSING_INVOICE_TEMPLATE), ErrorCodes.MISSING_INVOICE_TEMPLATE);

                var userCompanies = userCompaniesList
                    .SingleOrDefault(details => details.Id == invoice.UserCompanyId);

                var userBankAccounts = userBankAccountsList
                    .SingleOrDefault(bankData => bankData.Id == invoice.UserBankAccountId);

                if (userCompanies is null || userBankAccounts is null)
                    throw new BusinessException(nameof(ErrorCodes.PROCESSING_EXCEPTION), ErrorCodes.PROCESSING_EXCEPTION);

                const string dateFormat = "yyyy-MM-dd";
                const string currencyFormat = "#,#.00";

                var template = Encoding.Default.GetString(templateData);
                var userFullAddress = $"{userCompanies.StreetAddress}, {userCompanies.PostalCode} {userCompanies.City}";
                var customerAddress = $"{invoice.StreetAddress}, {invoice.PostalCode} {invoice.City}";

                var newInvoice = template
                    .Replace("{{F1}}", userCompanies.CompanyName)
                    .Replace("{{F2}}", userFullAddress)
                    .Replace("{{F3}}", userCompanies.VatNumber)
                    .Replace("{{F4}}", userCompanies.EmailAddress)
                    .Replace("{{F5}}", userCompanies.PhoneNumber)
                    .Replace("{{F6}}", invoice.InvoiceNumber)
                    .Replace("{{F7}}", invoice.ValueDate.ToString(dateFormat))
                    .Replace("{{F8}}", invoice.DueDate.ToString(dateFormat))
                    .Replace("{{F9}}", invoice.PaymentTerms.ToString())
                    .Replace("{{F22}}", userCompanies.CurrencyCode.ToString().ToUpper())
                    .Replace("{{F10}}", invoice.CustomerName)
                    .Replace("{{F11}}", invoice.CustomerVatNumber)
                    .Replace("{{F12}}", customerAddress)
                    .Replace("{{F23}}", userBankAccounts.BankName)
                    .Replace("{{F24}}", userBankAccounts.SwiftNumber)
                    .Replace("{{F25}}", userBankAccounts.AccountNumber)
                    .Replace("{{F26}}", invoice.PaymentStatus.ToString())
                    .Replace("{{F27}}", invoice.PaymentType.ToString());

                var rowTemplate = Regex.Match(template, @"(?<=<row-template>)((.|\n)*)(?=<\/row-template>)");
                var totalAmount = 0.0m;

                var batchInvoiceItems = invoiceItemsList
                    .Where(items => items.BatchInvoiceId == invoice.Id)
                    .ToList();

                if (!batchInvoiceItems.Any())
                    throw new BusinessException(nameof(ErrorCodes.PROCESSING_EXCEPTION), ErrorCodes.PROCESSING_EXCEPTION);

                var invoiceItems = new StringBuilder();
                foreach (var item in batchInvoiceItems)
                {
                    totalAmount += item.GrossAmount;
                    invoiceItems.Append(rowTemplate.Value
                        .Replace("{{F13}}", item.ItemText)
                        .Replace("{{F14}}", item.ItemQuantity.ToString())
                        .Replace("{{F15}}", item.ItemQuantityUnit)
                        .Replace("{{F16}}", item.ItemAmount.ToString(currencyFormat))
                        .Replace("{{F17}}", item.ItemDiscountRate.ToString())
                        .Replace("{{F18}}", item.ValueAmount.ToString(currencyFormat))
                        .Replace("{{F19}}", item.VatRate.ToString())
                        .Replace("{{F20}}", item.GrossAmount.ToString(currencyFormat)));
                }

                newInvoice = newInvoice
                    .Replace(rowTemplate.Value, invoiceItems.ToString())
                    .Replace("{{F21}}", totalAmount.ToString(currencyFormat))
                    .Replace("<row-template>", string.Empty)
                    .Replace("</row-template>", string.Empty);

                var issuedInvoiceData = new IssuedInvoiceData
                {
                    InvoiceContent = newInvoice,
                    CurrentInvoice = invoice,
                    InvoiceCollection = issuedInvoices,
                    BatchInvoicesProcessing = processing,
                    ProcessingTimer = timer
                };

                await LogIssuedInvoice(issuedInvoiceData, cancellationToken);
            }
            catch (BusinessException exception)
            {
                var error = new ProcessingError
                {
                    Error = exception.Message,
                    InnerError = exception.InnerException?.Message ?? string.Empty,
                    InvoiceNumber = invoice.InvoiceNumber,
                    Timer = timer,
                    ProcessingObject = processing
                };

                await LogProcessingFailed(error, cancellationToken);
            }
        }

        if (issuedInvoices.Any())
        {
            await _databaseContext.IssuedInvoices.AddRangeAsync(issuedInvoices, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        _loggerService.LogInformation($"Issued invoices: {issuedInvoices.Count}.");
    }

    /// <summary>
    /// Returns processing status of invoices to be generated.
    /// </summary>
    /// <param name="processBatchKey">Unique ID of batch process.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Processing Status object.</returns>
    /// <exception cref="BusinessException">Throws an error code INVALID_PROCESSING_BATCH_KEY.</exception>
    public async Task<ProcessingStatus> GetBatchInvoiceProcessingStatus(Guid processBatchKey, CancellationToken cancellationToken = default)
    {
        var processing = await _databaseContext.BatchInvoicesProcessing
            .AsNoTracking()
            .Where(processing => processing.Id == processBatchKey)
            .SingleOrDefaultAsync(cancellationToken);

        if (processing == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_PROCESSING_BATCH_KEY), ErrorCodes.INVALID_PROCESSING_BATCH_KEY);

        return new ProcessingStatus
        {
            Status = processing.Status,
            CreatedAt = processing.CreatedAt,
            BatchProcessingTime = processing.BatchProcessingTime ?? TimeSpan.Zero
        };
    }

    /// <summary>
    /// Returns generated invoice data of given type.
    /// </summary>
    /// <param name="invoiceNumber">Binary representation of generated invoice.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Invoice (binary content).</returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<InvoiceData> GetIssuedInvoice(string invoiceNumber, CancellationToken cancellationToken = default)
    {
        var invoice = await _databaseContext.IssuedInvoices
            .AsNoTracking()
            .Where(invoices => invoices.InvoiceNumber == invoiceNumber)
            .SingleOrDefaultAsync(cancellationToken);

        if (invoice == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_INVOICE_NUMBER), ErrorCodes.INVALID_INVOICE_NUMBER);

        return new InvoiceData
        {
            Number = invoice.InvoiceNumber,
            ContentData = invoice.InvoiceData,
            ContentType = invoice.ContentType,
            GeneratedAt = invoice.GeneratedAt
        };
    }

    private async Task<(List<BatchInvoices> invoices, List<BatchInvoiceItems> invoiceItemsList, List<InvoiceTemplates> invoiceTemplates)> GetInvoiceData
        (IEnumerable<Guid> processingList, CancellationToken cancellationToken)
    {
        var invoicesIds = new HashSet<Guid>(processingList);

        var invoices = await _databaseContext.BatchInvoices
            .AsNoTracking()
            .Where(batchInvoices => invoicesIds.Contains(batchInvoices.ProcessBatchKey))
            .ToListAsync(cancellationToken);

        var invoiceItemsIds = new HashSet<Guid>(invoices.Select(batchInvoices => batchInvoices.Id));

        var invoiceItemsList = await _databaseContext.BatchInvoiceItems
            .AsNoTracking()
            .Where(batchInvoiceItems => invoiceItemsIds.Contains(batchInvoiceItems.BatchInvoiceId))
            .ToListAsync(cancellationToken);

        var templateNames = invoices
            .Select(batchInvoices => batchInvoices.InvoiceTemplateName)
            .ToList();

        var invoiceTemplates = await _databaseContext.InvoiceTemplates
            .AsNoTracking()
            .Where(templates => templateNames.Contains(templates.Name))
            .Where(templates => !templates.IsDeleted)
            .ToListAsync(cancellationToken);

        return (invoices, invoiceItemsList, invoiceTemplates);
    }

    private async Task<(List<UserCompanies> userCompanies, List<UserBankAccounts> userBankAccounts)> GetUserData(
        IEnumerable<BatchInvoices> invoices, CancellationToken cancellationToken)
    {
        var userIds = new HashSet<Guid>(invoices.Select(batchInvoices => batchInvoices.UserId));

        var userCompanies = await _databaseContext.UserCompanies
            .AsNoTracking()
            .Where(details => userIds.Contains(details.UserId))
            .ToListAsync(cancellationToken);

        var userBankAccounts = await _databaseContext.UserBankAccounts
            .AsNoTracking()
            .Where(bankData => userIds.Contains(bankData.UserId))
            .ToListAsync(cancellationToken);

        return (userCompanies, userBankAccounts);
    }

    private async Task LogIssuedInvoice(IssuedInvoiceData issuedInvoiceData, CancellationToken cancellationToken)
    {
        var issuedInvoice = new IssuedInvoices
        {
            UserId = issuedInvoiceData.CurrentInvoice.UserId,
            InvoiceNumber = issuedInvoiceData.CurrentInvoice.InvoiceNumber,
            InvoiceData = Encoding.Default.GetBytes(issuedInvoiceData.InvoiceContent),
            ContentType = "text/html",
            GeneratedAt = _dateTimeService.Now
        };

        issuedInvoiceData.InvoiceCollection.Add(issuedInvoice);
        issuedInvoiceData.ProcessingTimer.Stop();
                    
        issuedInvoiceData.BatchInvoicesProcessing.Status = ProcessingStatuses.Finished;
        issuedInvoiceData.BatchInvoicesProcessing.BatchProcessingTime = issuedInvoiceData.ProcessingTimer.Elapsed;
                    
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task LogProcessingStarted(BatchInvoices currentInvoice, BatchInvoicesProcessing processing, Stopwatch timer, CancellationToken cancellationToken)
    {
        timer.Start();
        _loggerService.LogInformation($"Start processing invoice number: {currentInvoice.InvoiceNumber}.");
        processing.Status = ProcessingStatuses.Started;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task LogProcessingFailed(ProcessingError error, CancellationToken cancellationToken)
    {
        error.Timer.Stop();
        _loggerService.LogError($"Invoice processing has failed. Invoice number: {error.InvoiceNumber}.");
        error.ProcessingObject.Status = ProcessingStatuses.Failed;
        error.ProcessingObject.BatchProcessingTime = error.Timer.Elapsed;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
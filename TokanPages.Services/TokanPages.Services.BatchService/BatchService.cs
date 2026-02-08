using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;
using TokanPages.Services.BatchService.Abstractions;
using TokanPages.Services.BatchService.Models;
using ProcessingStatus = TokanPages.Backend.Domain.Enums.ProcessingStatus;

namespace TokanPages.Services.BatchService;

internal sealed class BatchService : IBatchService
{
    private readonly IInvoicingRepository _invoicingRepository;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    public BatchService(IDateTimeService dateTimeService, ILoggerService loggerService, IInvoicingRepository invoicingRepository)
    {
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _invoicingRepository = invoicingRepository;
    }

    /// <summary>
    /// Place an order for invoice processing. 
    /// </summary>
    /// <param name="orderDetails">Desired invoice data.</param>
    /// <returns>Process Batch Key.</returns>
    public async Task<Guid> OrderInvoiceBatchProcessing(IEnumerable<OrderDetail> orderDetails)
    {
        var processingId = await _invoicingRepository.CreateBatchInvoiceProcessing();
        var invoices = new List<BatchInvoiceDto>();
        var invoiceItems = new List<BatchInvoiceItemDto>();

        foreach (var order in orderDetails)
        {
            var batchInvoiceId = Guid.NewGuid();
            invoices.Add(new BatchInvoiceDto
            {
                Id = batchInvoiceId,
                InvoiceNumber = order.InvoiceNumber,
                VoucherDate = order.VoucherDate ?? _dateTimeService.Now,
                ValueDate = order.ValueDate ?? _dateTimeService.Now,
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
                ProcessBatchKey = processingId,
                UserId = order.UserId,
                UserCompanyId = order.UserCompanyId,
                UserBankAccountId = order.UserBankAccountId
            });

            foreach (var item in order.InvoiceItems)
            {
                invoiceItems.Add(new BatchInvoiceItemDto
                {
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

        await _invoicingRepository.CreateBatchInvoice(invoices);
        await _invoicingRepository.CreateBatchInvoiceItem(invoiceItems);

        _loggerService.LogInformation($"Invoice batch processing has been ordered (ProcessBatchKey: {processingId}).");
        return processingId;
    }

    /// <summary>
    /// Processes all outstanding invoices that have status 'new'.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task ProcessOutstandingInvoices(CancellationToken cancellationToken = default)
    {
        var processingList = await _invoicingRepository.GetBatchInvoiceProcessingByStatus(ProcessingStatus.New);
        if (processingList.Count == 0)
        {
            _loggerService.LogInformation("No new invoices to process.");
            return;
        }

        var processingIdList = processingList.Select(processing => processing.Id).ToList();
        var processingIds = new HashSet<Guid>(processingIdList);
        var invoices = await _invoicingRepository.GetBatchInvoicesByIds(processingIds);

        var invoiceIdList = invoices.Select(invoice => invoice.Id).ToList();
        var invoiceIds = new HashSet<Guid>(invoiceIdList);
        var invoiceItemsList = await _invoicingRepository.GetBatchInvoiceItemsByIds(invoiceIds);

        var templateNames = invoices
            .Select(invoice => invoice.InvoiceTemplateName)
            .ToList();

        var uniqueTemplateNames = new HashSet<string>(templateNames);
        var invoiceTemplates = await _invoicingRepository.GetInvoiceTemplatesByNames(uniqueTemplateNames);

        var userIds = new HashSet<Guid>(invoices.Select(batchInvoices => batchInvoices.UserId));
        var userCompaniesList = await _invoicingRepository.GetUserCompanies(userIds);
        var userBankAccountsList = await _invoicingRepository.GetUserBankAccounts(userIds);

        var issuedInvoices = new List<IssuedInvoiceDto>();
        foreach (var invoice in invoices)
        {
            var timer = new Stopwatch();
            var processing = await _invoicingRepository.GetBatchInvoiceProcessingByKey(invoice.ProcessBatchKey);

            ThrowIfNull(processing);

            await LogProcessingStarted(processing!.Id, invoice.InvoiceNumber, timer);
            try
            {
                var templateData = invoiceTemplates
                    .Where(templates => templates.Name == invoice.InvoiceTemplateName)
                    .Select(templates => templates.Data)
                    .SingleOrDefault();

                ThrowIfNull(templateData, ErrorCodes.MISSING_INVOICE_TEMPLATE);

                var userCompanies = userCompaniesList
                    .SingleOrDefault(details => details.Id == invoice.UserCompanyId);

                ThrowIfNull(userCompanies);

                var userBankAccounts = userBankAccountsList
                    .SingleOrDefault(bankData => bankData.Id == invoice.UserBankAccountId);

                ThrowIfNull(userBankAccounts);

                const string dateFormat = "yyyy-MM-dd";
                const string currencyFormat = "#,#.00";

                var template = Encoding.Default.GetString(templateData!);
                var userFullAddress = $"{userCompanies!.StreetAddress}, {userCompanies.PostalCode} {userCompanies.City}";
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
                    .Replace("{{F23}}", userBankAccounts!.BankName)
                    .Replace("{{F24}}", userBankAccounts.SwiftNumber)
                    .Replace("{{F25}}", userBankAccounts.AccountNumber)
                    .Replace("{{F26}}", invoice.PaymentStatus.ToString())
                    .Replace("{{F27}}", invoice.PaymentType.ToString());

                var rowTemplate = Regex.Match(template, @"(?<=<row-template>)((.|\n)*)(?=<\/row-template>)");
                var totalAmount = 0.0m;

                var batchInvoiceItems = invoiceItemsList
                    .Where(items => items.BatchInvoiceId == invoice.Id)
                    .ToList();

                ThrowIfEmpty(batchInvoiceItems);

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

                timer.Stop();

                issuedInvoices.Add(new IssuedInvoiceDto
                {
                    UserId = invoice.UserId,
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceData = Encoding.Default.GetBytes(newInvoice)
                });

                await _invoicingRepository.UpdateBatchInvoiceProcessingById(new BatchInvoiceProcessingDto
                {
                    ProcessingId = invoice.Id,
                    ProcessingStatus = ProcessingStatus.Finished,
                    ProcessingTime = timer.Elapsed
                });
            }
            catch (BusinessException exception)
            {
                await LogProcessingFailed(processing.Id, exception.Message, timer);
            }
        }

        await _invoicingRepository.CreateIssuedInvoice(issuedInvoices);
        _loggerService.LogInformation($"Issued invoices: {issuedInvoices.Count}.");
    }

    private static void ThrowIfNull(object? @object, string? errorMessage = default)
    {
        var message = errorMessage ?? ErrorCodes.PROCESSING_EXCEPTION;
        if (@object is null)
            throw new BusinessException(nameof(ErrorCodes.PROCESSING_EXCEPTION), message);
    }

    private static void ThrowIfEmpty(IEnumerable<object> list, string? errorMessage = default)
    {
        var message = errorMessage ?? ErrorCodes.PROCESSING_EXCEPTION;
        if (!list.Any())
            throw new BusinessException(nameof(ErrorCodes.PROCESSING_EXCEPTION), message);
    }

    private async Task LogProcessingStarted(Guid batchInvoiceProcessingId, string message, Stopwatch timer)
    {
        timer.Start();

        _loggerService.LogInformation($"Start processing invoice number: {message}.");

        await _invoicingRepository.UpdateBatchInvoiceProcessingById(new BatchInvoiceProcessingDto
        {
            ProcessingId = batchInvoiceProcessingId,
            ProcessingStatus = ProcessingStatus.Started
        });
    }

    private async Task LogProcessingFailed(Guid batchInvoiceProcessingId, string message, Stopwatch timer)
    {
        timer.Stop();
        var elapsed = timer.Elapsed;

        _loggerService.LogError($"Invoice processing has failed. Invoice number: {message}.");

        await _invoicingRepository.UpdateBatchInvoiceProcessingById(new BatchInvoiceProcessingDto
        {
            ProcessingId = batchInvoiceProcessingId,
            ProcessingStatus = ProcessingStatus.Failed,
            ProcessingTime = elapsed
        });
    }
}
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Batches.Commands;
using TokanPages.Invoicing.Dto.Batches;
using Models = TokanPages.Backend.Application.Batches.Commands.Models;

namespace TokanPages.Invoicing.Controllers.Mappers;

/// <summary>
/// Batch mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class BatchMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Batch object.</param>
    /// <returns>Command object.</returns>
    public static OrderInvoiceBatchCommand MapToOrderInvoiceBatchCommandRequest(OrderInvoiceBatchDto model) => new()
    {
        UserId = model.UserId,
        UserCompanyId = model.UserCompanyId,
        UserBankAccountId = model.UserBankAccountId,
        OrderDetails = model.OrderDetails.Select(order
            => new Models.OrderDetailBase<Models.InvoiceItemBase>
        {
            VoucherDate = order.VoucherDate,
            ValueDate = order.ValueDate,
            PaymentTerms = order.PaymentTerms,
            PaymentType = order.PaymentType,
            PaymentStatus = order.PaymentStatus,
            CompanyName = order.CompanyName,
            CompanyVatNumber = order.CompanyVatNumber,
            CountryCode = order.CountryCode,
            City = order.City,
            StreetAddress = order.StreetAddress,
            PostalCode = order.PostalCode,
            PostalArea = order.PostalArea,
            InvoiceTemplateName = order.InvoiceTemplateName,
            CurrencyCode = order.CurrencyCode,
            InvoiceItems = order.InvoiceItems.Select(invoice => new Models.InvoiceItemBase
            {
                ItemText = invoice.ItemText,
                ItemQuantity = invoice.ItemQuantity,
                ItemQuantityUnit = invoice.ItemQuantityUnit,
                ItemAmount = invoice.ItemAmount,
                ItemDiscountRate = invoice.ItemDiscountRate,
                VatRate = invoice.VatRate,
                CurrencyCode = invoice.CurrencyCode
            })
        })
    };
}
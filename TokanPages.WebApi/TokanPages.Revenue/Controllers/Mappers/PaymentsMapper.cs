using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Application.Revenue.Models;
using TokanPages.Backend.Application.Revenue.Models.Sections;
using TokanPages.Revenue.Dto.Payments;

namespace TokanPages.Revenue.Controllers.Mappers;

/// <summary>
/// Payment mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PaymentsMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Payment object.</param>
    /// <returns>Command object.</returns>
    public static CreatePaymentCommand MapToCreatePaymentCommand(CreatePaymentDto model) => new()
    {
        UserId = model.UserId,
        Request = new PaymentRequest
        {
            NotifyUrl = model.NotifyUrl,
            ContinueUrl = model.ContinueUrl,
            CustomerIp = model.CustomerIp,
            MerchantPosId = model.MerchantPosId,
            CardOnFile = model.CardOnFile,
            Recurring = model.Recurring,
            Description = model.Description,
            TotalAmount = model.TotalAmount,
            CurrencyCode = model.CurrencyCode,
            ExtOrderId = model.ExtOrderId,
            Products = model.Products?.Select(dto => new Product
            {
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            }),
            Buyer = new Buyer
            {
                Email = model.Buyer?.Email ?? string.Empty,
                Phone = model.Buyer?.Phone ?? string.Empty,
                FirstName = model.Buyer?.FirstName ?? string.Empty,
                LastName = model.Buyer?.LastName ?? string.Empty,
                Language = model.Buyer?.Language ?? string.Empty
            },
            PayMethods = new PayMethods
            {
                PayMethod = new PayMethod
                {
                    Card = new Card
                    {
                        Number = model.PayMethods?.PayMethod?.Card?.Number,
                        ExpirationMonth = model.PayMethods?.PayMethod?.Card?.ExpirationMonth,
                        ExpirationYear = model.PayMethods?.PayMethod?.Card?.ExpirationYear,
                        Cvv = model.PayMethods?.PayMethod?.Card?.Cvv
                    },
                    Type = model.PayMethods?.PayMethod?.Type,
                    Value = model.PayMethods?.PayMethod?.Value
                }
            },
            ThreeDsAuthentication = new Authentication
            {
                Recurring = new Recurring
                {
                    Frequency = model.ThreeDsAuthentication?.Recurring?.Frequency,
                    Expiry = model.ThreeDsAuthentication?.Recurring?.Expiry
                }
            }
        }
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Payment object.</param>
    /// <returns>Command object.</returns>
    public static CreatePaymentDefaultCommand MapToCreatePaymentDefaultCommand(CreatePaymentDefaultDto model) => new()
    {
        UserId = model.UserId,
        Request = new PaymentRequest
        {
            NotifyUrl = model.NotifyUrl,
            ContinueUrl = model.ContinueUrl,
            CustomerIp = model.CustomerIp,
            MerchantPosId = model.MerchantPosId,
            CardOnFile = model.CardOnFile,
            Recurring = model.Recurring,
            Description = model.Description,
            TotalAmount = model.TotalAmount,
            CurrencyCode = model.CurrencyCode,
            ExtOrderId = model.ExtOrderId,
            Products = model.Products?.Select(dto => new Product
            {
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            }),
            Buyer = new Buyer
            {
                Email = model.Buyer?.Email ?? string.Empty,
                Phone = model.Buyer?.Phone ?? string.Empty,
                FirstName = model.Buyer?.FirstName ?? string.Empty,
                LastName = model.Buyer?.LastName ?? string.Empty,
                Language = model.Buyer?.Language ?? string.Empty
            },
            PayMethods = new PayMethods
            {
                PayMethod = new PayMethod
                {
                    Card = new Card
                    {
                        Number = model.PayMethods?.PayMethod?.Card?.Number,
                        ExpirationMonth = model.PayMethods?.PayMethod?.Card?.ExpirationMonth,
                        ExpirationYear = model.PayMethods?.PayMethod?.Card?.ExpirationYear,
                        Cvv = model.PayMethods?.PayMethod?.Card?.Cvv
                    },
                    Type = model.PayMethods?.PayMethod?.Type,
                    Value = model.PayMethods?.PayMethod?.Value
                }
            },
            ThreeDsAuthentication = new Authentication
            {
                Recurring = new Recurring
                {
                    Frequency = model.ThreeDsAuthentication?.Recurring?.Frequency,
                    Expiry = model.ThreeDsAuthentication?.Recurring?.Expiry
                }
            }
        }
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Notification object.</param>
    /// <returns>Command object.</returns>
    public static PostNotificationCommand MapToPostNotificationCommand(PostNotificationDto model) => new()
    {
        Order = new Order
        {
            OrderId = model.Order?.OrderId,
            ExtOrderId = model.Order?.ExtOrderId,
            OrderCreateDate = model.Order?.OrderCreateDate,
            NotifyUrl = model.Order?.NotifyUrl,
            CustomerIp = model.Order?.CustomerIp,
            MerchantPosId = model.Order?.MerchantPosId,
            Description = model.Order?.Description,
            CurrencyCode = model.Order?.CurrencyCode,
            TotalAmount = model.Order?.TotalAmount,
            Buyer = new Buyer
            {
                Email = model.Order?.Buyer?.Email,
                Phone = model.Order?.Buyer?.Phone,
                FirstName = model.Order?.Buyer?.FirstName,
                LastName = model.Order?.Buyer?.LastName,
                Language = model.Order?.Buyer?.Language
            },
            PayMethod = new PayMethod
            {
                Card = new Card
                {
                    Number = model.Order?.PayMethod?.Card?.Number,
                    ExpirationMonth = model.Order?.PayMethod?.Card?.ExpirationMonth,
                    ExpirationYear = model.Order?.PayMethod?.Card?.ExpirationYear
                },
                Type = model.Order?.PayMethod?.Type,
                Value = model.Order?.PayMethod?.Value
            },
            Products = model.Order?.Products?.Select(dto => new Product
            {
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            }),
            Status = model.Order?.Status
        },
        LocalReceiptDateTime = model.LocalReceiptDateTime,
        Properties = model.Properties?.Select(dto => new Property
        {
            Name = dto.Name,
            Value = dto.Value
        })
    };
}
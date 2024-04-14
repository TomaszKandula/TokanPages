using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.PayUService.Models;
using TokanPages.Services.UserService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using BuyerInput = TokanPages.Services.PayUService.Models.Sections.Buyer;
using CardInput = TokanPages.Services.PayUService.Models.Sections.Card;
using PayMethodInput = TokanPages.Services.PayUService.Models.Sections.PayMethod;
using PayMethodsInput = TokanPages.Services.PayUService.Models.Sections.PayMethods;
using ProductInput = TokanPages.Services.PayUService.Models.Sections.Product;
using StatusInput = TokanPages.Services.PayUService.Models.Sections.Status;
using AuthenticationInput = TokanPages.Services.PayUService.Models.Sections.Authentication;
using RecurringInput = TokanPages.Services.PayUService.Models.Sections.Recurring;

using CardOutput = TokanPages.Backend.Application.Revenue.Models.Sections.Card;
using PayMethodOutput = TokanPages.Backend.Application.Revenue.Models.Sections.PayMethod;
using PayMethodsOutput = TokanPages.Backend.Application.Revenue.Models.Sections.PayMethods;
using StatusOutput = TokanPages.Backend.Application.Revenue.Models.Sections.Status;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class CreatePaymentCommandHandler : RequestHandler<CreatePaymentCommand, CreatePaymentCommandResult>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IPaymentService _paymentService;

    private readonly IConfiguration _configuration;

    public CreatePaymentCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService, IPaymentService paymentService, 
        IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _paymentService = paymentService;
        _configuration = configuration;
    }

    public override async Task<CreatePaymentCommandResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var ipAddress = _userService.GetRequestIpAddress();
        var merchantPosId = _configuration.GetValue<string>("Pmt_MerchantPosId");
        var order = request.Request;

        var cardTokenResponse = await _paymentService.GenerateCardToken(new GenerateCardTokenInput
        {
            PosId = merchantPosId,
            Type = "MULTI",
            Card = new CardInput
            {
                Number = order.PayMethods?.PayMethod?.Card?.Number,
                ExpirationMonth = order.PayMethods?.PayMethod?.Card?.ExpirationMonth,
                ExpirationYear = order.PayMethods?.PayMethod?.Card?.ExpirationYear,
                Cvv = order.PayMethods?.PayMethod?.Card?.Cvv
            }
        }, cancellationToken);

        var input = new PostOrderInput
        {
            NotifyUrl = order.NotifyUrl,
            ContinueUrl = order.ContinueUrl,
            CustomerIp = order.CustomerIp ?? ipAddress,
            MerchantPosId = order.MerchantPosId ?? merchantPosId,
            CardOnFile = order.CardOnFile,
            Description = order.Description,  
            TotalAmount = order.TotalAmount,
            CurrencyCode = order.CurrencyCode,
            ExtOrderId = order.ExtOrderId,
            Products = order.Products?.Select(product => new ProductInput
            {
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity
            }),
            Buyer = new BuyerInput
            {
                Email = order.Buyer?.Email,
                Phone = order.Buyer?.Phone,
                FirstName = order.Buyer?.FirstName,
                LastName = order.Buyer?.LastName,
                Language = order.Buyer?.Language
            },
            PayMethods = new PayMethodsInput
            {
                PayMethod = new PayMethodInput
                {
                    Type = "CARD_TOKEN",
                    Value = cardTokenResponse.Value
                }
            }
        };

        var postOrderResponse = await _paymentService.PostOrder(input, cancellationToken);
        var userPayments = await DatabaseContext.UserPayments
            .Where(payments => payments.UserId == request.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userPayments is not null)
        {
            userPayments.PmtOrderId = postOrderResponse.OrderId;
            userPayments.PmtStatus = postOrderResponse.Status?.StatusCode;
            userPayments.PmtType = postOrderResponse.PayMethods?.PayMethod?.Type;
            userPayments.PmtToken = postOrderResponse.PayMethods?.PayMethod?.Value;
            userPayments.ModifiedAt = _dateTimeService.Now;
            userPayments.ModifiedBy = user.Id;
            userPayments.ExtOrderId = order.ExtOrderId;
        }
        else
        {
            var userPayment = new UserPayment
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                PmtOrderId = postOrderResponse.OrderId,
                PmtStatus = postOrderResponse.Status?.StatusCode,
                PmtType = postOrderResponse.PayMethods?.PayMethod?.Type,
                PmtToken = postOrderResponse.PayMethods?.PayMethod?.Value,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = user.Id,
                ExtOrderId = order.ExtOrderId
            };

            await DatabaseContext.UserPayments.AddAsync(userPayment, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation("New user payment has been registered within the system.");

        return new CreatePaymentCommandResult
        {
            Status = new StatusOutput
            {
                StatusCode = postOrderResponse.Status?.StatusCode,
                StatusDesc = postOrderResponse.Status?.StatusDesc
            },
            RedirectUri = postOrderResponse.RedirectUri
        };
    }
}
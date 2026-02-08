using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.PayUService.Models;
using TokanPages.Services.UserService.Abstractions;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;
using BuyerInput = TokanPages.Services.PayUService.Models.Sections.Buyer;
using CardInput = TokanPages.Services.PayUService.Models.Sections.Card;
using PayMethodInput = TokanPages.Services.PayUService.Models.Sections.PayMethod;
using PayMethodsInput = TokanPages.Services.PayUService.Models.Sections.PayMethods;
using ProductInput = TokanPages.Services.PayUService.Models.Sections.Product;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class CreatePaymentDefaultCommandHandler : RequestHandler<CreatePaymentDefaultCommand, string>
{
    private readonly IUserService _userService;

    private readonly IPayUService _payUService;

    private readonly IRevenueRepository _revenueRepository;

    private readonly AppSettingsModel _appSettings;

    public CreatePaymentDefaultCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IPayUService payUService, IOptions<AppSettingsModel> options, IRevenueRepository revenueRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _payUService = payUService;
        _revenueRepository = revenueRepository;
        _appSettings = options.Value;
    }

    public override async Task<string> Handle(CreatePaymentDefaultCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var ipAddress = _userService.GetRequestIpAddress();
        var merchantPosId = _appSettings.PmtMerchantPosId;
        var order = request.Request;

        var cardTokenResponse = await _payUService.GenerateCardToken(new GenerateCardTokenInput
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
            MerchantPosId = order.MerchantPosId,
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

        var response = await _payUService.PostOrderDefault(input, cancellationToken);
        var userPayments = await _revenueRepository.GetUserPayment(user.Id);
        if (userPayments is not null)
        {
            var userPayment = new UpdateUserPaymentDto
            {
                PmtOrderId = string.Empty,
                PmtStatus = "AWAITING",
                PmtType = order.PayMethods?.PayMethod?.Type ?? string.Empty,
                PmtToken = order.PayMethods?.PayMethod?.Value ?? string.Empty,
                ModifiedBy = user.Id,
                CreatedAt = userPayments.CreatedAt,
                CreatedBy = userPayments.CreatedBy,
                ExtOrderId = order.ExtOrderId ?? string.Empty
            };

            await _revenueRepository.UpdateUserPayment(userPayment);
        }
        else
        {
            var userPayment = new CreateUserPaymentDto
            {
                UserId = user.Id,
                ExtOrderId = order.ExtOrderId ?? string.Empty,
                PmtOrderId = string.Empty,
                PmtStatus = "AWAITING",
                PmtType = order.PayMethods?.PayMethod?.Type ?? string.Empty,
                PmtToken = order.PayMethods?.PayMethod?.Value ?? string.Empty,
                CreatedBy = user.Id
            };

            await _revenueRepository.CreateUserPayment(userPayment);
        }

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation("New user payment has been registered within the system.");

        return response;
    }
}
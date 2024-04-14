using TokanPages.Backend.Application.Revenue.Models.Sections;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderDetailsQueryHandler : RequestHandler<GetOrderDetailsQuery, GetOrderDetailsQueryResult>
{
    private readonly IUserService _userService;
    
    private readonly IPayUService _payUService;

    public GetOrderDetailsQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IPayUService payUService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _payUService = payUService;
    }

    public override async Task<GetOrderDetailsQueryResult> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var output = await _payUService.GetOrderDetails(request.OrderId, cancellationToken);

        const string info = "Order details have been returned from payment provider. Active user ID:";
        LoggerService.LogInformation($"{info} {user.Id}.");

        return new GetOrderDetailsQueryResult
        {
            Orders = output.Orders?.Select(order => new Order
            {
                OrderId  = order.OrderId,
                ExtOrderId  = order.ExtOrderId,
                OrderCreateDate  = order.OrderCreateDate,
                NotifyUrl  = order.NotifyUrl,
                CustomerIp  = order.CustomerIp,
                MerchantPosId  = order.MerchantPosId,
                Description  = order.Description,
                CurrencyCode  = order.CurrencyCode,
                TotalAmount  = order.TotalAmount,
                Buyer  = new Buyer
                {
                    Email = order.Buyer?.Email,
                    Phone = order.Buyer?.Phone,
                    FirstName = order.Buyer?.FirstName,
                    LastName = order.Buyer?.LastName,
                    Language = order.Buyer?.Language
                },
                Products  = order.Products?.Select(product => new Product
                {
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity
                }),
                Status  = order.Status
            }),
            Status = new Status
            {
                StatusCode = output.Status?.StatusCode,
                StatusDesc = output.Status?.StatusDesc
            },
            Properties = output.Properties?.Select(property => new Property
            {
                Name = property.Name,
                Value = property.Value
            })
        };
    }
}
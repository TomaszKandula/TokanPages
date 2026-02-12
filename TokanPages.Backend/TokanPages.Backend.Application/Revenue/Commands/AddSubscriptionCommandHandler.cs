using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class AddSubscriptionCommandHandler : RequestHandler<AddSubscriptionCommand, AddSubscriptionCommandResult>
{
    private readonly IUserService _userService;

    private readonly IRevenueRepository _revenueRepository;
    
    private readonly IUserRepository _userRepository;

    public AddSubscriptionCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IRevenueRepository revenueRepository, IUserRepository userRepository)
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _revenueRepository = revenueRepository;
        _userRepository = userRepository;
    }

    public override async Task<AddSubscriptionCommandResult> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var currency = request.UserCurrency ?? "pln";
        var language = request.UserLanguage ?? "pol";

        var userInfo = await _userRepository.GetUserDetails(request.UserId);
        if (userInfo is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var price = await _revenueRepository.GetSubscriptionPrice(request.SelectedTerm, language, currency);
        if (price is null)
            throw new BusinessException(nameof(ErrorCodes.PRICE_NOT_FOUND), ErrorCodes.PRICE_NOT_FOUND);

        var userSubscription = await _revenueRepository.GetUserSubscription(userInfo.UserId);
        var extCustomerId = Guid.NewGuid().ToString("N");
        var extOrderId = Guid.NewGuid().ToString("N");

        AddSubscriptionCommandResult result;
        if (userSubscription is not null)
        {
            var subscription = new UpdateUserSubscriptionDto
            {
                AutoRenewal = true,
                Term = request.SelectedTerm,
                TotalAmount = price.Price,
                CurrencyIso = price.CurrencyIso,
                ExtCustomerId = extCustomerId,
                ExtOrderId = extOrderId,
                ModifiedBy = userInfo.UserId,
                IsActive = userSubscription.IsActive,
                CompletedAt =  userSubscription.CompletedAt,
                ExpiresAt =  userSubscription.ExpiresAt
            };

            result = new AddSubscriptionCommandResult
            {
                UserId = userInfo.UserId,
                SubscriptionId = userSubscription.Id,
                ExtOrderId = extOrderId,
                Email = userInfo.EmailAddress,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };
            
            await _revenueRepository.UpdateUserSubscription(subscription);
        }
        else
        {
            var subscriptionId = Guid.NewGuid();
            var subscription = new CreateUserSubscriptionDto
            {
                Id = subscriptionId,
                UserId = userInfo.UserId,
                Term = request.SelectedTerm,
                TotalAmount = price.Price,
                CurrencyIso = price.CurrencyIso,
                ExtCustomerId = extCustomerId,
                ExtOrderId = extOrderId,
                CreatedBy = userInfo.UserId
            };

            result = new AddSubscriptionCommandResult
            {
                UserId = subscription.UserId,
                SubscriptionId = subscriptionId,
                ExtOrderId = subscription.ExtOrderId,
                Email = userInfo.EmailAddress,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };

            await _revenueRepository.CreateUserSubscription(subscription);
        }

        LoggerService.LogInformation("New user subscription has been registered.");

        return result;
    }
}
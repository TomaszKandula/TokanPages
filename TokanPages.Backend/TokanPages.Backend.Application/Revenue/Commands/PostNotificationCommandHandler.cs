using TokanPages.Backend.Application.Notifications.Web.Command;
using TokanPages.Backend.Application.Revenue.Models;
using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class PostNotificationCommandHandler : RequestHandler<PostNotificationCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IRevenueRepository _revenueRepository;
    
    private readonly IMediator _mediator;

    public PostNotificationCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IJsonSerializer jsonSerializer, IMediator mediator, IRevenueRepository revenueRepository)
        : base(operationDbContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _jsonSerializer = jsonSerializer;
        _mediator = mediator;
        _revenueRepository = revenueRepository;
    }

    public override async Task<Unit> Handle(PostNotificationCommand request, CancellationToken cancellationToken)
    {
        var order = request.Order;
        var orderId = order?.OrderId;
        var extOrderId = order?.ExtOrderId;
        var status = order?.Status;

        var userPayment = await _revenueRepository.GetUserPayment(extOrderId ?? string.Empty);
        if (userPayment is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var updatePayment = new UpdateUserPaymentDto
        {
            PmtOrderId = orderId ?? string.Empty,
            PmtStatus = status ?? string.Empty,
            PmtType = userPayment.PmtType,
            PmtToken = userPayment.PmtToken,
            ModifiedBy = Guid.Empty,
            CreatedAt =  userPayment.CreatedAt,
            CreatedBy = userPayment.CreatedBy,
            ExtOrderId = userPayment.ExtOrderId
        };

        await _revenueRepository.UpdateUserPayment(updatePayment);

        var userId = userPayment.UserId;
        var userSubscription = await _revenueRepository.GetUserSubscription(userId);

        var details = $"OrderID: {orderId}, ExtOrderID: {extOrderId}, UserID: {userId}";
        if (userSubscription is null)
        {
            LoggerService.LogWarning($"Received payment for user that have no information on subscription! {details}.");
        }
        else
        {
            var getStatus = status?.ToUpper() ?? string.Empty;
            if (getStatus == "COMPLETED")
            {
                var updateSubscription = new UpdateUserSubscriptionDto
                {
                    AutoRenewal = userSubscription.AutoRenewal,
                    Term = userSubscription.Term,
                    TotalAmount = userSubscription.TotalAmount,
                    CurrencyIso = userSubscription.CurrencyIso,
                    ExtCustomerId = userSubscription.ExtCustomerId,
                    ExtOrderId = userSubscription.ExtOrderId,
                    ModifiedBy = userId,
                    IsActive = true,
                    CompletedAt = _dateTimeService.Now,
                    ExpiresAt = _dateTimeService.Now.AddDays((int)userSubscription.Term)
                };

                await _revenueRepository.UpdateUserSubscription(updateSubscription);
                LoggerService.LogInformation($"Subscription activated. {details}.");

                var history = new CreateUserPaymentHistoryDto
                {
                    UserId = userId,
                    Amount = userSubscription.TotalAmount,
                    CurrencyIso = userSubscription.CurrencyIso,
                    Term = userSubscription.Term,
                    CreatedBy = userId
                };

                await _revenueRepository.CreateUserPaymentsHistory(history);

                var payload = new SubscriptionNotification
                {
                    PaymentStatus = getStatus,
                    IsActive = userSubscription.IsActive,
                    AutoRenewal = userSubscription.AutoRenewal,
                    TotalAmount = userSubscription.TotalAmount,
                    CurrencyIso = userSubscription.CurrencyIso,
                    ExtCustomerId = userSubscription.ExtCustomerId,
                    ExtOrderId = userSubscription.ExtOrderId,
                    ExpiresAt = userSubscription.ExpiresAt
                };

                await NotifyWebApp(userId, payload, cancellationToken);
            }
            else
            {
                userSubscription.IsActive = false;
                LoggerService.LogWarning($"Cannot activate subscription, payment status is '{status}'. {details}.");

                var payload = new SubscriptionNotification { PaymentStatus = getStatus };
                await NotifyWebApp(userId, payload, cancellationToken);
            }
        }

        LoggerService.LogInformation($"Subscription updated upon received payment notification. {details}.");
        return Unit.Value;
    }

    private async Task NotifyWebApp(Guid userId, SubscriptionNotification notification, CancellationToken cancellationToken)
    {
        var notify = new NotifyRequestCommand
        {
            UserId = userId,
            ExternalPayload = _jsonSerializer.Serialize(notification),
            Handler = "payment_status"
        };

        await _mediator.Send(notify, cancellationToken);
        LoggerService.LogInformation("Payment completed. Web application notified.");
    }
}
using TokanPages.Backend.Application.NotificationsWeb.Command;
using TokanPages.Backend.Application.Users.Models.Subscription;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebSocketService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class PostNotificationCommandHandler : RequestHandler<PostNotificationCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly INotificationService _notificationService;

    private readonly IJsonSerializer _jsonSerializer;

    public PostNotificationCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        INotificationService notificationService, IJsonSerializer jsonSerializer): base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _notificationService = notificationService;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<Unit> Handle(PostNotificationCommand request, CancellationToken cancellationToken)
    {
        var order = request.Order;
        var orderId = order?.OrderId;
        var extOrderId = order?.ExtOrderId;
        var status = order?.Status;

        var userPayment = await DatabaseContext.UserPayments
            .Where(payments => payments.ExtOrderId == extOrderId)
            .SingleOrDefaultAsync(cancellationToken);

        userPayment.PmtOrderId = orderId;
        userPayment.PmtStatus = status;
        userPayment.ModifiedAt = _dateTimeService.Now;
        userPayment.ModifiedBy = Guid.Empty;

        var userId = userPayment.UserId;
        var userSubscription = await DatabaseContext.UserSubscriptions
            .Where(subscriptions => subscriptions.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

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
                userSubscription.IsActive = true;
                userSubscription.CompletedAt = _dateTimeService.Now;
                userSubscription.ExpiresAt = _dateTimeService.Now.AddDays((int)userSubscription.Term);
                LoggerService.LogInformation($"Subscription activated. {details}.");

                var history = new UserPaymentHistory
                {
                    UserId = userId,
                    Amount = userSubscription.TotalAmount,
                    CurrencyIso = userSubscription.CurrencyIso,
                    Term = userSubscription.Term,
                    CreatedBy = userId,
                    CreatedAt = _dateTimeService.Now
                };

                await DatabaseContext.UserPaymentsHistory.AddAsync(history, cancellationToken);
                await DatabaseContext.SaveChangesAsync(cancellationToken);
                
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

        await DatabaseContext.SaveChangesAsync(cancellationToken);
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

        var handler = new NotifyRequestCommandHandler(DatabaseContext, LoggerService, _notificationService, _jsonSerializer);
        await handler.Handle(notify, cancellationToken);
        LoggerService.LogInformation("Payment completed. Web application notified.");
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class ResetUserPasswordCommandHandler : RequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    private readonly IUserService _userService;

    public ResetUserPasswordCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, IOptions<AppSettingsModel> options, 
        IUserService userService) : base(operationDbContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _appSettings = options.Value;
        _userService = userService;
    }

    public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await OperationDbContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var resetId = Guid.NewGuid();
        var resetMaturity = _appSettings.LimitResetMaturity;

        user.CryptedPassword = string.Empty;
        user.ResetId = resetId;
        user.ResetIdEnds = _dateTimeService.Now.AddMinutes(resetMaturity);
        user.ModifiedAt = _dateTimeService.Now;
        user.ModifiedBy = user.Id;

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(resetMaturity);

        var messageId = await PrepareNotificationUncommitted(cancellationToken);
        await CommitAllChanges(cancellationToken);
        await SendNotification(messageId, request.LanguageId, request.EmailAddress!, resetId, expirationDate, cancellationToken);

        return Unit.Value;
    }

    private async Task CommitAllChanges(CancellationToken cancellationToken = default) 
        => await OperationDbContext.SaveChangesAsync(cancellationToken);

    private async Task<Guid> PrepareNotificationUncommitted(CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        await OperationDbContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        return messageId;
    }

    private async Task SendNotification(Guid messageId, string languageId, string emailAddress, Guid resetId, DateTime expirationDate, CancellationToken cancellationToken)
    {
        var configuration = new ResetPasswordConfiguration
        {
            LanguageId = languageId,
            MessageId = messageId,
            EmailAddress = emailAddress,
            ResetId = resetId,
            ExpirationDate = expirationDate
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}
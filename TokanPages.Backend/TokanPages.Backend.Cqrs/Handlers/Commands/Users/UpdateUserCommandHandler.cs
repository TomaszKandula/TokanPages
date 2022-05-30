namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Domain.Entities;
using MediatR;

public class UpdateUserCommandHandler : Cqrs.RequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;
        
    public UpdateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
    }

    public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id ?? await _userService.GetUserId(cancellationToken) ?? Guid.Empty;
        if (userId == Guid.Empty)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var currentUser = await DatabaseContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .Where(users => users.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (currentUser is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var emailCollection = await DatabaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id != userId)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        await UpdateUser(currentUser, request, cancellationToken);
        await UpdateUserInfo(userId, request, cancellationToken);
        return Unit.Value;
    }

    private async Task UpdateUser(Users currentUser, UpdateUserCommand request, CancellationToken cancellationToken = default)
    {
        currentUser.IsActivated = request.IsActivated;
        currentUser.UserAlias = request.UserAlias ?? currentUser.UserAlias;
        currentUser.EmailAddress = request.EmailAddress ?? currentUser.EmailAddress;
        currentUser.ModifiedAt = _dateTimeService.Now;
        currentUser.ModifiedBy = currentUser.Id;
        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateUserInfo(Guid userId, UpdateUserCommand request, CancellationToken cancellationToken = default)
    {
        var userInfo = await DatabaseContext.UserInfo
            .Where(info => info.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null)
        {
            var newUserInfo = new UserInfo
            {
                UserId = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserAboutText = request.ShortBio,
                CreatedBy = userId,
                CreatedAt = _dateTimeService.Now
            };

            await DatabaseContext.UserInfo.AddAsync(newUserInfo, cancellationToken);
        }
        else
        {
            userInfo.FirstName = request.FirstName ?? userInfo.FirstName;
            userInfo.LastName = request.LastName ?? userInfo.LastName;
            userInfo.UserAboutText = request.ShortBio ?? userInfo.UserAboutText;
            userInfo.ModifiedBy = userId;
            userInfo.ModifiedAt = _dateTimeService.Now;
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }
}
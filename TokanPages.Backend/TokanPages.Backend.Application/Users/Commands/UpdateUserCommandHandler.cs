using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserCommandHandler : RequestHandler<UpdateUserCommand, Unit>
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
        var user = await _userService.GetActiveUser(request.Id, true, cancellationToken);

        var emails = await DatabaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id != user.Id)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .ToListAsync(cancellationToken);

        if (emails.Any())
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        await UpdateUser(user, request, cancellationToken);
        await UpdateUserInfo(user.Id, request, cancellationToken);
        return Unit.Value;
    }

    private async Task UpdateUser(Domain.Entities.Users user, UpdateUserCommand request, CancellationToken cancellationToken = default)
    {
        user.IsActivated = request.IsActivated;
        user.UserAlias = request.UserAlias ?? user.UserAlias;
        user.EmailAddress = request.EmailAddress ?? user.EmailAddress;
        user.ModifiedAt = _dateTimeService.Now;
        user.ModifiedBy = user.Id;
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
                UserAboutText = request.UserAboutText,
                UserImageName = request.UserImageName,
                UserVideoName = request.UserVideoName,
                CreatedBy = userId,
                CreatedAt = _dateTimeService.Now
            };

            await DatabaseContext.UserInfo.AddAsync(newUserInfo, cancellationToken);
        }
        else
        {
            userInfo.FirstName = request.FirstName ?? userInfo.FirstName;
            userInfo.LastName = request.LastName ?? userInfo.LastName;
            userInfo.UserAboutText = request.UserAboutText ?? userInfo.UserAboutText;
            userInfo.UserImageName = request.UserImageName ?? userInfo.UserImageName;
            userInfo.UserVideoName = request.UserVideoName ?? userInfo.UserVideoName;
            userInfo.ModifiedBy = userId;
            userInfo.ModifiedAt = _dateTimeService.Now;
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }
}
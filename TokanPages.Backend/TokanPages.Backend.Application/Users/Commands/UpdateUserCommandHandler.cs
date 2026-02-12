using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserCommandHandler : RequestHandler<UpdateUserCommand, UpdateUserCommandResult>
{
    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;
        
    public UpdateUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IUserRepository userRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public override async Task<UpdateUserCommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.Id);
        var shouldVerify = !string.Equals(request.EmailAddress, user.EmailAddress, StringComparison.CurrentCultureIgnoreCase);
        var canChangeEmailAddress = await _userRepository.IsEmailAddressAvailableForChange(user.UserId, user.EmailAddress);
        if (!canChangeEmailAddress)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var updateUser = new UpdateUserDto
        {
            UserId = user.UserId,
            UserAlias = request.UserAlias ?? user.UserAlias,
            EmailAddress = request.EmailAddress ?? user.EmailAddress,
            IsActivated = request.IsActivated ?? user.IsActivated,
            IsVerified = shouldVerify
        };

        var updateInformation = new UpdateUserInformationDto
        {
            UserId = user.UserId,
            FirstName = request.FirstName ?? user.FirstName,
            LastName = request.LastName ?? user.LastName,
            UserAboutText = request.Description ?? user.UserAboutText,
            UserImageName = request.UserImageName ?? user.UserImageName,
            UserVideoName = request.UserVideoName ?? user.UserVideoName
        };

        await _userRepository.UpdateUser(updateUser);
        await _userRepository.UpdateUserInformation(updateInformation);

        return new UpdateUserCommandResult
        {
            ShouldVerifyEmail = shouldVerify
        };
    }
}
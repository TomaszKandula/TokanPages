namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared.Services.DateTimeService;
    using Services.UserServiceProvider;
    using Services.CipheringService;
    using Identity.Authorization;
    using Shared.Resources;
    using Core.Exceptions;
    using Core.Extensions;
    using Core.Logger;
    using Database;
    using MediatR;
    using Shared;
    
    public class UpdateUserPasswordCommandHandler : TemplateHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;

        private readonly ICipheringService FCipheringService;

        private readonly IDateTimeService FDateTimeService;
        
        private readonly ILogger FLogger;
        
        public UpdateUserPasswordCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider, 
            ICipheringService ACipheringService, IDateTimeService ADateTimeService, ILogger ALogger)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
            FCipheringService = ACipheringService;
            FDateTimeService = ADateTimeService;
            FLogger = ALogger;
        }

        public override async Task<Unit> Handle(UpdateUserPasswordCommand ARequest, CancellationToken ACancellationToken)
        {
            var LIsResetId = ARequest.ResetId != null;
            var LUsers = await FDatabaseContext.Users
                .WhereIfElse(!LIsResetId, 
                    AUser => AUser.Id == ARequest.Id, 
                    AUser => AUser.ResetId == ARequest.ResetId) 
                .ToListAsync(ACancellationToken);

            if (!LIsResetId)
            {
                if (!LUsers.Any())
                    throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

                var LHasRoleEverydayUser = await FUserServiceProvider.HasRoleAssigned($"{Roles.EverydayUser}") ?? false;
                var LHasRoleGodOfAsgard = await FUserServiceProvider.HasRoleAssigned($"{Roles.GodOfAsgard}") ?? false;

                if (!LHasRoleEverydayUser && !LHasRoleGodOfAsgard)
                    throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            }

            if (!LUsers.Any())
                throw new BusinessException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

            var LCurrentUser = LUsers.First();
            if (LIsResetId && FDateTimeService.Now > LCurrentUser.ResetIdEnds)
                throw new BusinessException(nameof(ErrorCodes.EXPIRED_RESET_ID), ErrorCodes.EXPIRED_RESET_ID);
            
            var LGetNewSalt = FCipheringService.GenerateSalt(Constants.CIPHER_LOG_ROUNDS);
            var LGetHashedPassword = FCipheringService.GetHashedPassword(ARequest.NewPassword, LGetNewSalt);
            
            LCurrentUser.ResetId = null;
            LCurrentUser.ResetIdEnds = null;
            LCurrentUser.CryptedPassword = LGetHashedPassword;
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            FLogger.LogInformation($"User password has been updated successfully (UserId: {LCurrentUser.Id}).");
            return await Task.FromResult(Unit.Value);
        }
    }
}
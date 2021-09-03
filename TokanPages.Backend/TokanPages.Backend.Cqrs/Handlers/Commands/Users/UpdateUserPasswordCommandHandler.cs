namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Services.UserServiceProvider;
    using Services.CipheringService;
    using Identity.Authorization;
    using Shared.Resources;
    using Core.Exceptions;
    using Core.Logger;
    using Database;
    using MediatR;
    using Shared;
    
    public class UpdateUserPasswordCommandHandler : TemplateHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;

        private readonly ICipheringService FCipheringService;
        
        private readonly ILogger FLogger;
        
        public UpdateUserPasswordCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider, ICipheringService ACipheringService, ILogger ALogger)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
            FCipheringService = ACipheringService;
            FLogger = ALogger;
        }

        public override async Task<Unit> Handle(UpdateUserPasswordCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .Where(AUser => AUser.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LUsers.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            if (ARequest.ResetId == null)
            {
                var LHasRoleEverydayUser = await FUserServiceProvider.HasRoleAssigned(Roles.EverydayUser.ToString()) ?? false;
                var LHasRoleGodOfAsgard = await FUserServiceProvider.HasRoleAssigned(Roles.GodOfAsgard.ToString()) ?? false;
                
                if (!LHasRoleEverydayUser || !LHasRoleGodOfAsgard)
                    throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            }

            var LCurrentUser = LUsers.First();

            if (LCurrentUser.ResetId == ARequest.ResetId)
                throw new BusinessException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

            LCurrentUser.ResetId = null;
            LCurrentUser.CryptedPassword = FCipheringService.GetHashedPassword(ARequest.NewPassword, FCipheringService.GenerateSalt(Constants.CIPHER_LOG_ROUNDS));
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            FLogger.LogInformation($"User password has been reset successfully (UserId: {ARequest.Id}).");
            return await Task.FromResult(Unit.Value);
        }
    }
}
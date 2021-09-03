namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Services.CipheringService;
    using Shared.Resources;
    using Core.Exceptions;
    using Core.Logger;
    using Database;
    using MediatR;
    using Shared;
    
    public class UpdateUserPasswordCommandHandler : TemplateHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly ICipheringService FCipheringService;
        
        private readonly ILogger FLogger;
        
        public UpdateUserPasswordCommandHandler(DatabaseContext ADatabaseContext, ICipheringService ACipheringService, ILogger ALogger)
        {
            FDatabaseContext = ADatabaseContext;
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
            
            var LCurrentUser = LUsers.First();
            LCurrentUser.ResetId = null;
            LCurrentUser.CryptedPassword = FCipheringService.GetHashedPassword(ARequest.NewPassword, FCipheringService.GenerateSalt(Constants.CIPHER_LOG_ROUNDS));
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MediatR;
    using Database;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Services.DateTimeService;
    
    public class ActivateUserCommandHandler : TemplateHandler<ActivateUserCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly DateTimeService FDateTimeService;
        
        private readonly ILogger FLogger;
        
        public ActivateUserCommandHandler(DatabaseContext ADatabaseContext, DateTimeService ADateTimeService, ILogger ALogger)
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
            FLogger = ALogger;
        }

        public override async Task<Unit> Handle(ActivateUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUser = await FDatabaseContext.Users
                .SingleOrDefaultAsync(AUsers => AUsers.ActivationId == ARequest.ActivationId, ACancellationToken);

            if (LUser == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

            if (LUser.ActivationIdEnds < FDateTimeService.Now)
                throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);
            
            LUser.IsActivated = true;
            LUser.ActivationId = null;
            LUser.ActivationIdEnds = null;

            FLogger.LogInformation($"User account has been activated, user ID: {LUser.Id}");
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
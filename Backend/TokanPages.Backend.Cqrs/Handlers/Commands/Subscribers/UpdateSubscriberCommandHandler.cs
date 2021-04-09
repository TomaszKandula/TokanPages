using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.DateTimeService;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    public class UpdateSubscriberCommandHandler : TemplateHandler<UpdateSubscriberCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IDateTimeService FDateTimeService;
        
        public UpdateSubscriberCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService) 
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
        }

        public override async Task<Unit> Handle(UpdateSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LSubscribers = await FDatabaseContext.Subscribers
                .Where(ASubscribers => ASubscribers.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LSubscribers.Any()) 
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            var LEmailCollection = await FDatabaseContext.Subscribers
                .AsNoTracking()
                .Where(AUsers => AUsers.Email == ARequest.Email)
                .ToListAsync(ACancellationToken);

            if (LEmailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var LCurrentSubscriber = LSubscribers.First();

            LCurrentSubscriber.Email = ARequest.Email;
            LCurrentSubscriber.Count = ARequest.Count ?? LCurrentSubscriber.Count;
            LCurrentSubscriber.IsActivated = ARequest.IsActivated ?? LCurrentSubscriber.IsActivated;
            LCurrentSubscriber.LastUpdated = FDateTimeService.Now;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}

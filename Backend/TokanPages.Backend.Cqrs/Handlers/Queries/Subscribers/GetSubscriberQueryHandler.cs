using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{

    public class GetSubscriberQueryHandler : IRequestHandler<GetSubscriberQuery, Domain.Entities.Subscribers>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetSubscriberQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Domain.Entities.Subscribers> Handle(GetSubscriberQuery ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentSubscriber = await FDatabaseContext.Subscribers.FindAsync(new object[] { ARequest.Id }, ACancellationToken);
            if (LCurrentSubscriber == null) 
            {
                throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
            }

            return LCurrentSubscriber;

        }

    }

}

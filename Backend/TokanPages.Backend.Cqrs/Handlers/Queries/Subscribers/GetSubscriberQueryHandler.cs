using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TokanPages.Backend.Database;

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
                // TODO: add error call
            }

            return LCurrentSubscriber;

        }

    }

}

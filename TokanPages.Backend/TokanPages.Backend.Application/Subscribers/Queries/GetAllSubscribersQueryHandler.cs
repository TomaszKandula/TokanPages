﻿namespace TokanPages.Backend.Application.Subscribers.Queries;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Utilities.LoggerService;

public class GetAllSubscribersQueryHandler : RequestHandler<GetAllSubscribersQuery, List<GetAllSubscribersQueryResult>>
{
    public GetAllSubscribersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllSubscribersQueryResult>> Handle(GetAllSubscribersQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Subscribers
            .AsNoTracking()
            .Select(subscribers => new GetAllSubscribersQueryResult 
            { 
                Id = subscribers.Id,
                Email = subscribers.Email,
                IsActivated = subscribers.IsActivated,
                NewsletterCount = subscribers.Count
            })
            .ToListAsync(cancellationToken);
    }
}
﻿namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;

public class GetUserQueryHandler : RequestHandler<GetUserQuery, GetUserQueryResult>
{
    public GetUserQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await DatabaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id == request.Id)
            .Select(users => new GetUserQueryResult
            {
                Id = users.Id,
                Email = users.EmailAddress,
                AliasName = users.UserAlias,
                IsActivated = users.IsActivated,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Registered = users.Registered,
                LastUpdated = users.LastUpdated,
                LastLogged = users.LastLogged
            })
            .ToListAsync(cancellationToken);

        if (!currentUser.Any())
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return currentUser.First();
    }
}
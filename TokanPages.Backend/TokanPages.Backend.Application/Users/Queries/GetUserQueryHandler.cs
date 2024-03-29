﻿using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryHandler : RequestHandler<GetUserQuery, GetUserQueryResult>
{
    public GetUserQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var query = await (from users in DatabaseContext.Users
            join userInfo in DatabaseContext.UserInfo
            on users.Id equals userInfo.UserId
            where users.Id == request.Id
            select new GetUserQueryResult
            {
                Id = users.Id,
                Email = users.EmailAddress,
                AliasName = users.UserAlias,
                IsActivated = users.IsActivated,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Registered = users.CreatedAt,
                LastUpdated = userInfo.ModifiedAt
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (query is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return query;
    }
}
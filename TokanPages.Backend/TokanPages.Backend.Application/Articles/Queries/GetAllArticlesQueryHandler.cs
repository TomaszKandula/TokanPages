﻿namespace TokanPages.Backend.Application.Articles.Queries;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Utilities.LoggerService;

public class GetAllArticlesQueryHandler : RequestHandler<GetAllArticlesQuery, List<GetAllArticlesQueryResult>>
{
    public GetAllArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.IsPublished == request.IsPublished)
            .Select(articles => new GetAllArticlesQueryResult 
            { 
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt
            })
            .OrderByDescending(articles => articles.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
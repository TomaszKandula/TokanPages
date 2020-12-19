﻿using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class RemoveArticleCommandHandler : TemplateHandler<RemoveArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public RemoveArticleCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(RemoveArticleCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(ARequest.Id);
            if (LCurrentArticle == null)
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            FDatabaseContext.Articles.Remove(LCurrentArticle);

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);

        }

    }

}

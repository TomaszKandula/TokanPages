namespace TokanPages.Backend.Application.Handlers.Queries.Assets;

using Microsoft.AspNetCore.Mvc;
using MediatR;

public class GetArticleAssetQuery : IRequest<FileContentResult>
{
    public string Id { get; set; } = "";

    public string AssetName { get; set; } = "";
}
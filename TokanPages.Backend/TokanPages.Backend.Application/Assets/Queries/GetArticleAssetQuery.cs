namespace TokanPages.Backend.Application.Assets.Queries;

using Microsoft.AspNetCore.Mvc;
using MediatR;

public class GetArticleAssetQuery : IRequest<FileContentResult>
{
    public string Id { get; set; } = "";

    public string AssetName { get; set; } = "";
}
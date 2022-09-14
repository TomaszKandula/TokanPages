using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetArticleAssetQuery : IRequest<FileContentResult>
{
    public string Id { get; set; } = "";

    public string AssetName { get; set; } = "";
}
namespace TokanPages.WebApi.Controllers.Proxy;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Enums;
using Services.Caching.Assets;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Assets;
using MediatR;

[ApiVersion("1.0")]
public class AssetsController : ApiBaseController
{
    private readonly IAssetsCache _assetsCache;

    public AssetsController(IMediator mediator,  IAssetsCache assetsCache) : base(mediator) => _assetsCache = assetsCache;

    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsset([FromQuery] string blobName, bool noCache = false)
        => await _assetsCache.GetAsset(blobName, noCache);

    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(GetAssetsListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetAssetsListQueryResult> GetAssetsList() 
        => await Mediator.Send(new GetAssetsListQuery());

    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticleAsset([FromQuery] string id, string assetName, bool noCache = false)
        => await _assetsCache.GetArticleAsset(id, assetName, noCache);
}
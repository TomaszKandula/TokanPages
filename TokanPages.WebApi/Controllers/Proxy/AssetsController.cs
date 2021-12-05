namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Services.Caching.Assets;

    [ApiVersion("1.0")]
    public class AssetsController : ProxyBaseController
    {
        private readonly IAssetsCache _assetsCache;

        public AssetsController(IAssetsCache assetsCache) => _assetsCache = assetsCache;

        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsset([FromQuery] string blobName, bool noCache = false)
            => await _assetsCache.GetAsset(blobName, noCache);

        [HttpGet("Article")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticleAsset([FromQuery] string id, string assetName, bool noCache = false)
            => await _assetsCache.GetArticleAsset(id, assetName, noCache);
    }
}
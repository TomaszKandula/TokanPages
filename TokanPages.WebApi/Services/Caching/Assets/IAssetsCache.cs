namespace TokanPages.WebApi.Services.Caching.Assets
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IAssetsCache
    {
        Task<IActionResult> GetAsset(string blobName, bool noCache = false);

        Task<IActionResult> GetArticleAsset(string id, string assetName, bool noCache = false);
    }
}
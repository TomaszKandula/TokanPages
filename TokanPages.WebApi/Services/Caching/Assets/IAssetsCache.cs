namespace TokanPages.WebApi.Services.Caching.Assets;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Assets cache contract
/// </summary>
public interface IAssetsCache
{
    /// <summary>
    /// Returns asset
    /// </summary>
    /// <param name="blobName">Full asset name</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns></returns>
    Task<IActionResult> GetAsset(string blobName, bool noCache = false);

    /// <summary>
    /// Returns asset associated to the article
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="assetName">Full asset name</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns></returns>
    Task<IActionResult> GetArticleAsset(string id, string assetName, bool noCache = false);
}
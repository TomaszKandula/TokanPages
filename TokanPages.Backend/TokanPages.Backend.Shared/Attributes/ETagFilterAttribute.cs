using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TokanPages.Backend.Shared.Configuration;

namespace TokanPages.Backend.Shared.Attributes;

/// <summary>
/// ETag filter implementation
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class ETagFilterAttribute : Attribute, IActionFilter
{
    private readonly int[] _statusCodes;

    /// <summary>
    /// ETag filter for endpoint returning an asset like image
    /// </summary>
    /// <param name="statusCodes">Status codes, the default value is 200 (OK)</param>
    public ETagFilterAttribute(params int[] statusCodes)
    {
        _statusCodes = statusCodes;
        if (statusCodes.Length == 0) _statusCodes = new[] { 200 };
    }

    /// <summary>
    /// Method is called when the action is invoked
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Leave it empty
    }

    /// <summary>
    /// Method is called after action is executed
    /// </summary>
    /// <param name="context">Action executed context</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.HttpContext.Request.Method != "GET") return;
        if (!((IList)_statusCodes).Contains(context.HttpContext.Response.StatusCode)) return;

        var content = JsonConvert.SerializeObject(context.Result);
        var requestPath = context.HttpContext.Request.Path.ToString();
        var etag = ETagGenerator.GetETag(requestPath, Encoding.UTF8.GetBytes(content));

        var containsKey = context.HttpContext.Request.Headers.ContainsKey("If-None-Match");
        var hasETag = context.HttpContext.Request.Headers["If-None-Match"].ToString() == etag;

        if (containsKey && hasETag)
            context.Result = new StatusCodeResult(304);

        context.HttpContext.Response.Headers.Add("ETag", new[] { etag });
    }
}

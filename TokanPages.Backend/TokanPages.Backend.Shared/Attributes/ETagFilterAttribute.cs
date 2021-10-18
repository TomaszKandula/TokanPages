namespace TokanPages.Backend.Shared.Attributes
{
    using System;
    using System.Text;
    using System.Collections;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Method)]
    public class ETagFilterAttribute : Attribute, IActionFilter
    {
        private const string Etag = "ETag";
        
        private const string CacheControl = "Cache-Control";

        private const string NoCache = "no-cache";

        private const string IfNoneMatch = "If-None-Match";

        private const string HttpGetMethod = "GET";

        private readonly int[] _statusCodes;

        public ETagFilterAttribute(params int[] statusCodes)
        {
            _statusCodes = statusCodes;

            if (statusCodes.Length == 0) 
                _statusCodes = new[] { 200 };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Leave it empty, do not process during execution
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method != HttpGetMethod) 
                return;

            if (!((IList) _statusCodes).Contains(context.HttpContext.Response.StatusCode)) 
                return;

            var content = context.Result.ToString();
            var requestPath = context.HttpContext.Request.Path.ToString();
            var eTag = ETagGenerator.GetETag(requestPath, Encoding.UTF8.GetBytes(content ?? string.Empty));

            var hasNoneMatchItem = context.HttpContext.Request.Headers.Keys.Contains(IfNoneMatch);
            var noneMatchHeader = context.HttpContext.Request.Headers[IfNoneMatch].ToString();

            if (hasNoneMatchItem && noneMatchHeader == eTag)
                context.Result = new StatusCodeResult(304);

            context.HttpContext.Response.Headers.Add(Etag, new[] { eTag });
            context.HttpContext.Response.Headers.Add(CacheControl,NoCache);
        }
    }
}
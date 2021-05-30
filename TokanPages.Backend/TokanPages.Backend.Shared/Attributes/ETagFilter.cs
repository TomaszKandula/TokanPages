using System;
using System.Text;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TokanPages.Backend.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ETagFilter : Attribute, IActionFilter
    {
        private const string ETAG = "ETag";
        
        private const string CACHE_CONTROL = "Cache-Control";

        private const string NO_CACHE = "no-cache";

        private const string IF_NONE_MATCH = "If-None-Match";

        private const string HTTP_GET_METHOD = "GET";

        private readonly int[] FStatusCodes;

        public ETagFilter(params int[] AStatusCodes)
        {
            FStatusCodes = AStatusCodes;

            if (AStatusCodes.Length == 0) 
                FStatusCodes = new[] { 200 };
        }

        public void OnActionExecuting(ActionExecutingContext AContext) { }

        public void OnActionExecuted(ActionExecutedContext AContext)
        {
            if (AContext.HttpContext.Request.Method != HTTP_GET_METHOD) 
                return;

            if (!((IList) FStatusCodes).Contains(AContext.HttpContext.Response.StatusCode)) 
                return;

            var LContent = AContext.Result.ToString();
            var LRequestPath = AContext.HttpContext.Request.Path.ToString();
            var LEtag = ETagGenerator.GetETag(LRequestPath, Encoding.UTF8.GetBytes(LContent ?? string.Empty));

            var LHasNoneMatchItem = AContext.HttpContext.Request.Headers.Keys.Contains(IF_NONE_MATCH);
            var LNoneMatchHeader = AContext.HttpContext.Request.Headers[IF_NONE_MATCH].ToString();
            
            if (LHasNoneMatchItem && LNoneMatchHeader == LEtag)
                AContext.Result = new StatusCodeResult(304);
                    
            AContext.HttpContext.Response.Headers.Add(ETAG, new[] { LEtag });
            AContext.HttpContext.Response.Headers.Add(CACHE_CONTROL,NO_CACHE);
        }
    }
}
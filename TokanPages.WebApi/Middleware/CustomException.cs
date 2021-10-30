namespace TokanPages.WebApi.Middleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.Core.Models;
    using Backend.Core.Exceptions;
    using Backend.Shared.Resources;
    using Configuration;
    
    [ExcludeFromCodeCoverage]
    public class CustomException
    {
        private readonly RequestDelegate _requestDelegate;
        
        public CustomException(RequestDelegate requestDelegate) 
            => _requestDelegate = requestDelegate;
        
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate.Invoke(httpContext);
            }
            catch (ValidationException validationException)
            {
                var applicationError = new ApplicationError(validationException.ErrorCode, validationException.Message, validationException.ValidationResult);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (BusinessException businessException)
            {
                var applicationError = new ApplicationError(businessException.ErrorCode, businessException.Message);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var applicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED, exception.Message);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private static Task WriteErrorResponse(HttpContext httpContext, ApplicationError applicationError, HttpStatusCode statusCode)
        {
            var result = JsonSerializer.Serialize(applicationError);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            CorsHeaders.Ensure(httpContext);
            return httpContext.Response.WriteAsync(result);
        }
    }
}
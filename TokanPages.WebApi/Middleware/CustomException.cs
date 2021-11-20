namespace TokanPages.WebApi.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.Core.Models;
    using Backend.Core.Exceptions;
    using Backend.Shared.Resources;
    using Newtonsoft.Json;
    
    [ExcludeFromCodeCoverage]
    public class CustomException
    {
        private readonly RequestDelegate _requestDelegate;
        
        public CustomException(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;
        
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
                var innerMessage = businessException.InnerException?.Message;
                var applicationError = new ApplicationError(businessException.ErrorCode, businessException.Message, innerMessage);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.UnprocessableEntity).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var innerMessage = exception.InnerException?.Message;
                var applicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), exception.Message, innerMessage);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private static Task WriteErrorResponse(HttpContext httpContext, ApplicationError applicationError, HttpStatusCode statusCode)
        {
            var result = JsonConvert.SerializeObject(applicationError);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            return httpContext.Response.WriteAsync(result);
        }
    }
}
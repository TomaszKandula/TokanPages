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
        private readonly RequestDelegate FRequestDelegate;
        
        public CustomException(RequestDelegate ARequestDelegate) 
            => FRequestDelegate = ARequestDelegate;
        
        public async Task Invoke(HttpContext AHttpContext)
        {
            try
            {
                await FRequestDelegate.Invoke(AHttpContext);
            }
            catch (ValidationException LValidationException)
            {
                var LApplicationError = new ApplicationError(LValidationException.ErrorCode, LValidationException.Message, LValidationException.ValidationResult);
                await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (BusinessException LBusinessException)
            {
                var LApplicationError = new ApplicationError(LBusinessException.ErrorCode, LBusinessException.Message);
                await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception LException)
            {
                var LApplicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED, LException.Message);
                await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private static Task WriteErrorResponse(HttpContext AHttpContext, ApplicationError AApplicationError, HttpStatusCode AStatusCode)
        {
            var LResult = JsonSerializer.Serialize(AApplicationError);
            AHttpContext.Response.ContentType = "application/json";
            AHttpContext.Response.StatusCode = (int)AStatusCode;
            CorsHeaders.Ensure(AHttpContext);
            return AHttpContext.Response.WriteAsync(LResult);
        }
    }
}
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Models;
using TokanPages.Backend.Shared.Resources;
using Newtonsoft.Json;

namespace TokanPages.Middleware
{

    public class ExceptionHandler
    {

        public async Task Invoke(HttpContext AHttpContext)
        {

            var LExceptionHandlerPathFeature = AHttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var LException = LExceptionHandlerPathFeature.Error;

            string LResult;           
            switch (LException) 
            {

                case ValidationException AException:
                    {
                        var LAppError = new ApplicationError(AException.ErrorCode, AException.Message, AException.ValidationResult);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                case BusinessException AException:
                    {
                        var LAppError = new ApplicationError(AException.ErrorCode, AException.Message);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                default:
                    {
                        var LAppError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                        LResult = JsonConvert.SerializeObject(LAppError);
                        AHttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    }

            }

            await AHttpContext.Response.WriteAsync(LResult);

        }

    }

}

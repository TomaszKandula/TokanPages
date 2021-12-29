namespace TokanPages.Backend.Core.Exceptions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Models;

[ExcludeFromCodeCoverage]
public class ApiVersionException : IErrorResponseProvider
{
    public IActionResult CreateResponse(ErrorResponseContext context)
    {
        const string errorCode = "INVALID_API_VERSION";
        const string errorMessage = "Provided API version seems to be invalid";

        var innerError = context.Message;
        var error = new ApplicationError(errorCode, errorMessage, innerError);
        var response = new ObjectResult(error) { StatusCode = StatusCodes.Status400BadRequest };

        return response;
    }
}
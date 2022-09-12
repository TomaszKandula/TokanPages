namespace TokanPages.Backend.Application.Logger.Queries;

using Microsoft.AspNetCore.Mvc;
using MediatR;

public class GetLogFileContentQuery : IRequest<FileContentResult>
{
    public string LogFileName { get; set; } = "";
}
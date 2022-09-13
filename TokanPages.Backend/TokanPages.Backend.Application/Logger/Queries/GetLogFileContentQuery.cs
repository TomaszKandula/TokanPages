using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Logger.Queries;

public class GetLogFileContentQuery : IRequest<FileContentResult>
{
    public string LogFileName { get; set; } = "";
}
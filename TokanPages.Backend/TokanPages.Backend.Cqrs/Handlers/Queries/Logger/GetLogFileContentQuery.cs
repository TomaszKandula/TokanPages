namespace TokanPages.Backend.Cqrs.Handlers.Queries.Logger;

using Microsoft.AspNetCore.Mvc;
using MediatR;

public class GetLogFileContentQuery : IRequest<FileContentResult>
{
    public string LogFileName { get; set; } = "";
}
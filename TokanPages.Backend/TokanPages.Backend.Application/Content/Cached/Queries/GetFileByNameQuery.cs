using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Content.Cached.Queries;

public class GetFileByNameQuery : IRequest<FileContentResult>
{
    public string? FileName { get; set; }
}
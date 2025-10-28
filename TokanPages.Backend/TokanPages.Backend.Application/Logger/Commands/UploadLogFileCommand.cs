using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Logger.Commands;

public class UploadLogFileCommand : IRequest<Unit>
{
    public string CatalogName { get; set; } = "";

    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}
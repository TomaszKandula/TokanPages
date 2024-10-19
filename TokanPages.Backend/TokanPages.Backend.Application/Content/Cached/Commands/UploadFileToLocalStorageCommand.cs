using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class UploadFileToLocalStorageCommand : IRequest<UploadFileToLocalStorageCommandResult>
{
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}
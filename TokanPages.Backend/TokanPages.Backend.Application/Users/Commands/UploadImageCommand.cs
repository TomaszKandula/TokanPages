using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadImageCommand : IRequest<UploadImageCommandResult>
{
    public bool SkipDb { get; set; }

    public string? Base64Data { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}
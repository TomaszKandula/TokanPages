using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddImageAssetCommand : IRequest<AddImageAssetCommandResult>
{
    public string? Base64Data { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}
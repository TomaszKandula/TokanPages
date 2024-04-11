using MediatR;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddVideoAssetCommand : IRequest<AddVideoAssetCommandResult>
{
    public ProcessingTarget Target { get; set; }

    public IFormFile? BinaryData { get; set; }
}
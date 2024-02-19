using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddVideoAssetCommand : IRequest<AddVideoAssetCommandResult>
{
    public IFormFile? BinaryData { get; set; }
}
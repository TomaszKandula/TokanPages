using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetImageAssetQuery : IRequest<FileContentResult>
{
    public string BlobName { get; set; } = "";
}
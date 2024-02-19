using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetImageAssetQuery : IRequest<FileContentResult>
{
    public string BlobName { get; set; } = "";
}
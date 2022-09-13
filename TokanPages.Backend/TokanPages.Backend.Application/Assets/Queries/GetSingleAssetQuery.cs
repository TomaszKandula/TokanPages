using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetSingleAssetQuery : IRequest<FileContentResult>
{
    public string BlobName { get; set; } = "";
}
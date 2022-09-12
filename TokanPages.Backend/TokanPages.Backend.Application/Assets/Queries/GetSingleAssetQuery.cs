namespace TokanPages.Backend.Application.Assets.Queries;

using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetSingleAssetQuery : IRequest<FileContentResult>
{
    public string BlobName { get; set; } = "";
}
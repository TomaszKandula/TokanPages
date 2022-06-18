namespace TokanPages.Backend.Cqrs.Handlers.Queries.Assets;

using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetSingleAssetQuery : IRequest<FileContentResult>
{
    public string BlobName { get; set; } = "";
}
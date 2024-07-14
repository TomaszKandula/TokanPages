using MediatR;
using TokanPages.Backend.Application.Content.Assets.Models;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetNonVideoAssetQuery : IRequest<ContentOutput>
{
    public string BlobName { get; set; } = "";

    public bool CanDownload { get; set; }
}
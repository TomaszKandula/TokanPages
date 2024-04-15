using MediatR;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoAssetQuery : IRequest<Unit>
{
    public string BlobName { get; set; } = "";
}
using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserVideoQuery : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string BlobName { get; set; } = "";
}
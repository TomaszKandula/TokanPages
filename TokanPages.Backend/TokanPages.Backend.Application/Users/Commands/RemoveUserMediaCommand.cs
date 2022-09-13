using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserMediaCommand : IRequest<Unit>
{
    public string UniqueBlobName { get; set; } = "";
}

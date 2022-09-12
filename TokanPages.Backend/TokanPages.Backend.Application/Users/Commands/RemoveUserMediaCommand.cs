namespace TokanPages.Backend.Application.Users.Commands;

using MediatR;

public class RemoveUserMediaCommand : IRequest<Unit>
{
    public string UniqueBlobName { get; set; } = "";
}

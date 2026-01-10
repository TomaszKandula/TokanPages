using MediatR;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserFileCommand : IRequest<Unit>
{
    public UserFileToReceive Type { get; set; }

    public string UniqueBlobName { get; set; } = "";
}
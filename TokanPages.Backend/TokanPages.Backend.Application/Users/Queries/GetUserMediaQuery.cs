using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserMediaQuery : IRequest<FileContentResult>
{
    public Guid Id { get; set; }

    public string BlobName { get; set; } = "";
}

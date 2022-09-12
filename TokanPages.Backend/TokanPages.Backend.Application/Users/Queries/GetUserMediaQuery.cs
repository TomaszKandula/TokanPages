namespace TokanPages.Backend.Application.Users.Queries;

using System;
using Microsoft.AspNetCore.Mvc;
using MediatR;

public class GetUserMediaQuery : IRequest<FileContentResult>
{
    public Guid Id { get; set; }

    public string BlobName { get; set; } = "";
}
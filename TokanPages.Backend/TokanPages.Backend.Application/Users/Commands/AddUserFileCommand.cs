using MediatR;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommand : IRequest<AddUserFileCommandResult>
{
    public UserFileToUpdate Type { get; set; }

    public IFormFile? BinaryData { get; set; }
}
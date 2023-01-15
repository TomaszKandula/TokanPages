using MediatR;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadUserMediaCommand : IRequest<UploadUserMediaCommandResult>
{
    public Guid? UserId { get; set; }

    public UserMedia MediaTarget { get; set; }

    public string MediaName { get; set; } = "";

    public string MediaType { get; set; } = "";

    public byte[] Data { get; set; } = Array.Empty<byte>();

    public string DataType { get; set; } = "";

    public bool SkipDb { get; set; }
}
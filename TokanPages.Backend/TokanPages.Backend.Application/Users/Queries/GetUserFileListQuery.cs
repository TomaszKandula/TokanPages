using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQuery : IRequest<GetUserFileListResult>
{
    public bool IsVideoFile { get; set; }
}
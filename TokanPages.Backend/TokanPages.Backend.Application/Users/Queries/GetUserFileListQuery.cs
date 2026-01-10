using MediatR;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQuery : IRequest<GetUserFileListQueryResult>
{
    public UserFile Type { get; set; }
}
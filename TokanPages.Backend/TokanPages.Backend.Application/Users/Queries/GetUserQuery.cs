using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQuery : IRequest<GetUserQueryResult>
{
    public Guid Id { get; set; }
}
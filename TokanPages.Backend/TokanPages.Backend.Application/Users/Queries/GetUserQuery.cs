namespace TokanPages.Backend.Application.Handlers.Queries.Users;

using System;
using MediatR;

public class GetUserQuery : IRequest<GetUserQueryResult>
{
    public Guid Id { get; set; }
}
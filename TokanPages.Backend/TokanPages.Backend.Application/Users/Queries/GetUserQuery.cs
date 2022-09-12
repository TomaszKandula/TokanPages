namespace TokanPages.Backend.Application.Users.Queries;

using System;
using MediatR;

public class GetUserQuery : IRequest<GetUserQueryResult>
{
    public Guid Id { get; set; }
}
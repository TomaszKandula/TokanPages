namespace TokanPages.Backend.Application.Handlers.Queries.Subscribers;

using System;
using MediatR;

public class GetSubscriberQuery : IRequest<GetSubscriberQueryResult>
{
    public Guid Id { get; set; }
}
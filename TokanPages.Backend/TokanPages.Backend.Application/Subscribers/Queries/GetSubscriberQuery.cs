namespace TokanPages.Backend.Application.Subscribers.Queries;

using System;
using MediatR;

public class GetSubscriberQuery : IRequest<GetSubscriberQueryResult>
{
    public Guid Id { get; set; }
}